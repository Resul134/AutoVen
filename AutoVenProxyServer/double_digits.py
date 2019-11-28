from sense_hat import SenseHat
import time
import math
​
s = SenseHat()
s.low_light = True
​
green = (0, 255, 0)
yellow = (255, 255, 0)
blue = (0, 0, 255)
red = (255, 0, 0)
white = (255, 255, 255)
nothing = (0, 0, 0)
pink = (255, 105, 180)
​
​
def digit_placement(digit_array, digit_place, digit_color):
    tmp_array = []
    for x in digit_array:
        tmp_array.append(nothing)
​
    if digit_place == 1:
        return digit_array
​
    if digit_place == 0:
        for i in range(len(digit_array)):
            if digit_array[i] == digit_color:
                tmp_array[i + 2] = digit_color
​
        return tmp_array
​
    if digit_place == 2:
        skip = 0
        for i in range(len(digit_array)):
            if digit_array[i] == digit_color:
                tmp_array[i + 4] = digit_color
​
        return tmp_array
​
​
def combine_digits(digit_ones, digit_tens, digit_color):
    new_array = []
    for x in digit_ones:
        new_array.append(x)
​
    for y in range(len(new_array)):
        if new_array[y] != digit_color and digit_tens[y] == digit_color:
            new_array[y] = digit_color
​
    return new_array
​
​
def digit_one(color, digit_place):
    X = color
    O = nothing
    digit = [
        O, O, X, O, O, O, O, O,
        O, X, X, O, O, O, O, O,
        O, O, X, O, O, O, O, O,
        O, O, X, O, O, O, O, O,
        O, O, X, O, O, O, O, O,
        O, O, X, O, O, O, O, O,
        O, O, X, O, O, O, O, O,
        O, X, X, X, O, O, O, O,
    ]
    digit = digit_placement(digit, digit_place, X)
​
    return digit
​
​
def digit_two(color, digit_place):
    X = color
    O = nothing
    digit = [
        O, X, X, O, O, O, O, O,
        X, O, O, X, O, O, O, O,
        O, O, O, X, O, O, O, O,
        O, O, O, X, O, O, O, O,
        O, O, X, O, O, O, O, O,
        O, X, O, O, O, O, O, O,
        X, O, O, O, O, O, O, O,
        X, X, X, X, O, O, O, O,
    ]
    digit = digit_placement(digit, digit_place, X)
    return digit
​
​
def digit_three(color, digit_place):
    X = color
    O = nothing
    digit = [
        O, X, X, O, O, O, O, O,
        X, O, O, X, O, O, O, O,
        O, O, O, X, O, O, O, O,
        O, X, X, O, O, O, O, O,
        O, O, O, X, O, O, O, O,
        O, O, O, X, O, O, O, O,
        X, O, O, X, O, O, O, O,
        O, X, X, O, O, O, O, O,
    ]
    digit = digit_placement(digit, digit_place, X)
    return digit
​
​
def digit_four(color, digit_place):
    X = color
    O = nothing
    digit = [
        X, O, O, X, O, O, O, O,
        X, O, O, X, O, O, O, O,
        X, O, O, X, O, O, O, O,
        X, O, O, X, O, O, O, O,
        O, X, X, X, O, O, O, O,
        O, O, O, X, O, O, O, O,
        O, O, O, X, O, O, O, O,
        O, O, O, X, O, O, O, O,
    ]
    digit = digit_placement(digit, digit_place, X)
    return digit
​
​
def digit_five(color, digit_place):
    X = color
    O = nothing
    digit = [
        X, X, X, X, O, O, O, O,
        X, O, O, O, O, O, O, O,
        X, O, O, O, O, O, O, O,
        O, X, X, X, O, O, O, O,
        O, O, O, X, O, O, O, O,
        O, O, O, X, O, O, O, O,
        O, O, O, X, O, O, O, O,
        X, X, X, O, O, O, O, O,
    ]
    digit = digit_placement(digit, digit_place, X)
    return digit
​
​
def digit_six(color, digit_place):
    X = color
    O = nothing
    digit = [
        O, X, X, O, O, O, O, O,
        X, O, O, O, O, O, O, O,
        X, O, O, O, O, O, O, O,
        X, X, X, O, O, O, O, O,
        X, O, O, X, O, O, O, O,
        X, O, O, X, O, O, O, O,
        X, O, O, X, O, O, O, O,
        O, X, X, O, O, O, O, O,
    ]
    digit = digit_placement(digit, digit_place, X)
    return digit
​
​
def digit_seven(color, digit_place):
    X = color
    O = nothing
    digit = [
        X, X, X, O, O, O, O, O,
        O, O, O, X, O, O, O, O,
        O, O, O, X, O, O, O, O,
        O, O, O, X, O, O, O, O,
        O, O, O, X, O, O, O, O,
        O, O, O, X, O, O, O, O,
        O, O, O, X, O, O, O, O,
        O, O, O, X, O, O, O, O,
    ]
    digit = digit_placement(digit, digit_place, X)
    return digit
​
​
def digit_eight(color, digit_place):
    X = color
    O = nothing
    digit = [
        O, X, X, O, O, O, O, O,
        X, O, O, X, O, O, O, O,
        X, O, O, X, O, O, O, O,
        O, X, X, O, O, O, O, O,
        X, O, O, X, O, O, O, O,
        X, O, O, X, O, O, O, O,
        X, O, O, X, O, O, O, O,
        O, X, X, O, O, O, O, O,
    ]
    digit = digit_placement(digit, digit_place, X)
    return digit
​
​
def digit_nine(color, digit_place):
    X = color
    O = nothing
    digit = [
        O, X, X, O, O, O, O, O,
        X, O, O, X, O, O, O, O,
        X, O, O, X, O, O, O, O,
        O, X, X, X, O, O, O, O,
        O, O, O, X, O, O, O, O,
        O, O, O, X, O, O, O, O,
        O, O, O, X, O, O, O, O,
        O, X, X, O, O, O, O, O,
    ]
    digit = digit_placement(digit, digit_place, X)
    return digit
​
​
def digit_zero(color, digit_place):
    X = color
    O = nothing
    digit = [
        O, X, X, O, O, O, O, O,
        X, O, O, X, O, O, O, O,
        X, O, O, X, O, O, O, O,
        X, O, O, X, O, O, O, O,
        X, O, O, X, O, O, O, O,
        X, O, O, X, O, O, O, O,
        X, O, O, X, O, O, O, O,
        O, X, X, O, O, O, O, O,
    ]
    digit = digit_placement(digit, digit_place, X)
    return digit
​
​
def error(color, digit_place):
    X = color
    O = nothing
    digit = [
        O, X, X, O, O, O, O, O,
        X, O, O, X, O, O, O, O,
        X, O, O, X, O, O, O, O,
        X, O, O, X, O, O, O, O,
        X, O, O, X, O, O, O, O,
        X, O, O, X, O, O, O, O,
        X, O, O, X, O, O, O, O,
        O, X, X, O, O, O, O, O,
    ]
    digit = digit_placement(digit, digit_place, X)
    return digit
​
​
def string_to_digit(string, color, placement):
    if string == "0":
        return digit_zero(color, placement)
    elif string == "1":
        return digit_one(color, placement)
    elif string == "2":
        return digit_two(color, placement)
    elif string == "3":
        return digit_three(color, placement)
    elif string == "4":
        return digit_four(color, placement)
    elif string == "5":
        return digit_five(color, placement)
    elif string == "6":
        return digit_six(color, placement)
    elif string == "7":
        return digit_seven(color, placement)
    elif string == "8":
        return digit_eight(color, placement)
    else:
        return digit_nine(color, placement)
​
​
def get_digit_array(number, color):
    string = str(number)
​
    if len(string) == 1:
        return string_to_digit(string, color, 0)
​
    elif len(string) == 2:
        digit_tens = string_to_digit(string[0], color, 1)
        digit_ones = string_to_digit(string[1], color, 2)
        return combine_digits(digit_tens, digit_ones, color)
​
    else:
        return error(color, 0)