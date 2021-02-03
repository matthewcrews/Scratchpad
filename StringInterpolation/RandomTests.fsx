
// let rng = System.Random()
// let x = rng.Next(1, 10)

// let maxCharacters = 5
// let longString = "AAAAAAAAAAAAAAAAAAAA"
// let shortString = longString.[..maxCharacters - 1]

let dirs = System.IO.Directory.EnumerateFiles(".")
let text = System.IO.File.ReadAllLines(".")