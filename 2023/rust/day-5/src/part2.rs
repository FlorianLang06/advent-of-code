use std::str::Lines;
use std::sync::{Arc, Mutex};
use std::sync::atomic::{AtomicUsize, Ordering};
use std::thread;
use std::time::Duration;

pub fn process(input: &str) -> u64 {
    let mut lines = input.lines();
    let seeds_line = match lines.next() {
        None => return 0,
        Some(line) => line,
    };

    let mut seeds_line_split = seeds_line.split(":");
    let _ = seeds_line_split.next();
    let seeds_string = match seeds_line_split.next() {
        None => return 0,
        Some(s) => s.trim(),
    };

    let seed_ranges = seeds_string.split(" ").map(|s| s.parse::<u64>()).filter(|option| option.is_ok()).map(|option| option.unwrap()).collect::<Vec<u64>>();

    let _ = lines.next();
    let _ = lines.next();

    let maps = read_maps(&mut lines);

    let mut last_value: u64 = 0;

    let end_result: Arc<Mutex<u64>> = Arc::new(Mutex::new(0u64));
    let finished_threads = Arc::new(AtomicUsize::new(1));

    let end_result_move = Arc::clone(&end_result);
    for i in 0..(seed_ranges.len() - 1) {
        let value = match seed_ranges.get(i) {
            None => return 0,
            Some(v) => v,
        };

        if i % 2 == 0 {
            last_value = *value;
        } else {
            let last_value = last_value.clone();
            let value = *value;
            let finished_threads = Arc::clone(&finished_threads);
            let maps = maps.clone();
            let end_result = Arc::clone(&end_result_move);
            thread::spawn(move || {
                let mut result = 0;
                for seed in last_value..(last_value + value - 1) {
                    let seed_result = handle_mappings(seed, &maps);
                    if seed_result < result || result == 0 {
                        result = seed_result;
                    }
                }


                let finished_threads_value = finished_threads.load(Ordering::Relaxed);
                finished_threads.store(finished_threads_value + 1, Ordering::Relaxed);

                let mut end_result_value = match end_result.lock() {
                    Ok(v) => v,
                    Err(_) => return
                };

                if *end_result_value > result || *end_result_value == 0{
                    *end_result_value = result;
                }
            });

        }
    }

    while finished_threads.load(Ordering::Relaxed) < (seed_ranges.len() / 2) {
        thread::sleep(Duration::from_millis(100));
    }

    let result = match end_result.lock() {
        Ok(v) => v.to_owned(),
        Err(_) => return 0,
    };
    return result;
}

fn handle_mappings(seed: u64, maps: &Vec<Map>) -> u64 {
    let mut result = seed;
    for map in maps {
        match map.mappings.iter().filter(|m| result >= m.source_start && m.source_start + m.length > result).next() {
            None => {}
            Some(mapping) => {
                let diff = result - mapping.source_start;
                result = mapping.destination_start + diff;
            }
        };
    }
    result
}

fn read_maps(lines: &mut Lines) -> Vec<Map> {
    let mut maps = Vec::<Map>::new();

    let mut current_map = Map::new();


    loop {
        let line = match lines.next() {
            None => {
                maps.push(current_map);
                return maps;
            }
            Some(l) => l,
        };
        if line == "" {
            let _ = lines.next();
            maps.push(current_map);
            current_map = Map::new();
            continue;
        }
        let mut line_split = line.split(" ");
        let destination_start = match line_split.next() {
            None => continue,
            Some(s) => {
                match s.parse::<u64>() {
                    Ok(n) => n,
                    Err(_) => continue
                }
            }
        };
        let source_start = match line_split.next() {
            None => continue,
            Some(s) => {
                match s.parse::<u64>() {
                    Ok(n) => n,
                    Err(_) => continue
                }
            }
        };
        let length = match line_split.next() {
            None => continue,
            Some(s) => {
                match s.parse::<u64>() {
                    Ok(n) => n,
                    Err(_) => continue
                }
            }
        };

        current_map.mappings.push(Mapping {
            source_start,
            destination_start,
            length,
        })
    }
}

#[derive(Clone)]
struct Map {
    mappings: Vec<Mapping>,
}

impl Map {
    fn new() -> Self {
        Self {
            mappings: Vec::<Mapping>::new(),
        }
    }
}

#[derive(Clone)]
struct Mapping {
    source_start: u64,
    destination_start: u64,
    length: u64,
}