pub fn process(input: &str) -> u64 {
    let mut lines = input.lines();
    let times = line_to_numbers(lines.next());
    let distances = line_to_numbers(lines.next());

    let mut result = 0u64;

    for i in 0..(times.len()) {
        let time = match times.get(i) {
            None => return 0,
            Some(t) => *t,
        };
        let distance = match distances.get(i) {
            None => return 0,
            Some(d) => *d,
        };

        let mut win_count = 0u64;

        for press_millis in 1..time {
            if (time - press_millis) * press_millis > distance {
                win_count = win_count + 1;
            }
        }
        if result == 0 {
            result = win_count;
        } else {
            result = win_count * result;
        }
    }

    return result;
}

fn line_to_numbers(line: Option<&str>) -> Vec<u64> {
    match line {
        None => Vec::<u64>::new(),
        Some(line) => {
            let mut split = line.split(":");
            let _ = split.next();
            let times_string = match split.next() {
                None => return Vec::<u64>::new(),
                Some(t) => t
            };
            times_string.split(" ").map(|s| s.parse::<u64>()).filter(|option| option.is_ok()).map(|option| option.unwrap()).collect::<Vec<u64>>()
        }
    }
}