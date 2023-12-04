pub fn process(input: &str) -> u32 {
    input.lines().map(|line| {
        let mut split_card = line.split(":");
        let _ = split_card.next();
        let numbers = split_card.next().unwrap();
        let mut split_numbers = numbers.split("|");
        let win_numbers = split_numbers.next().unwrap();
        let my_numbers = split_numbers.next().unwrap();
        let win_numbers = win_numbers
        .split(|c| c == ' ')
        .filter_map(|number| number.trim().parse::<u8>().ok()).collect::<Vec<_>>();
        let my_numbers = my_numbers
            .split(|c| c == ' ')
            .filter_map(|number| number.trim().parse::<u8>().ok());

        let count = my_numbers.filter(|my|win_numbers.contains(my)).count();

        if count == 0 {
            0
        } else {
            let mut result = 1;

            for _ in 0..(count - 1) {
                result = result * 2;
            }
            result
        }
    }).sum()
}