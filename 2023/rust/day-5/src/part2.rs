use std::str::Lines;

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

    let mut end_result = Vec::<u64>::new();

    for i in 0..(seed_ranges.len() - 1) {
        let value = match seed_ranges.get(i) {
            None => return 0,
            Some(v) => v,
        };

        if i % 2 == 0 {
            last_value = *value;
        } else {
            let mut result = 0;
            for seed in last_value..(last_value + value - 1) {
                let seed_result = handle_mappings(seed, &maps);
                if seed_result < result || result == 0 {
                    result = seed_result;
                }
            }

            end_result.push(result);
        }
    }


    match end_result.iter().min() {
        None => 0,
        Some(min) => *min
    }
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