pub fn process(input: &str) -> u64 {
    let mut lines = input.lines();
    let time = line_to_numbers(lines.next());
    let distance = line_to_numbers(lines.next());

    let mut result = 0u64;

    for press_millis in 1..time {
        if (time - press_millis) * press_millis > distance {
            result = result + 1;
        }
    }


    return result;
}

fn line_to_numbers(line: Option<&str>) -> u64 {
    match line {
        None => 0,
        Some(line) => {
            let mut split = line.split(":");
            let _ = split.next();
            let times_string = match split.next() {
                None => return 0,
                Some(t) => t
            };
            match times_string.replace(" ", "").parse::<u64>() {
                Ok(n) => n,
                Err(_) => 0
            }
        }
    }
}