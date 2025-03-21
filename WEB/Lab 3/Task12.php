<?php
// ------------------------------- Task 12 -------------------------------

function sum($m, $i = 0)
{
    if($i >= count($m)) return 0;
    return sum($m, $i + 1) + $m[$i];
}

$massive = [1,2,3,4,5,6];
echo sum($massive) / count($massive);
echo "<br>";

// ------------------------------- Task 12 -------------------------------

function sumRange($num = 1)
{
    if($num > 100) return 0;
    return sumRange($num + 1) + $num;
}

echo sumRange();
echo "<br>";


// ------------------------------- Task 12 -------------------------------

function getSqrt($m, $t = [], $i = 0): array
{
    if($i >= count($m)) return $t;
    $t[] = sqrt($m[$i]);
    return getSqrt($m, $t, $i + 1);
}

$massive = [4, 9, 16, 25, 36];
print_r(getSqrt($massive));
echo "<br>";


// ------------------------------- Task 12 -------------------------------

function getMassive($t = [], $i = 0): array
{
    if($i >= 26) return $t;
    $t[chr($i + 97)] = $i + 1;
    return getMassive($t, $i + 1);
}

$massive = getMassive();
print_r($massive);
echo "<br>";


// ------------------------------- Task 12 -------------------------------

function sumPair($arr, $i = 0): int
{
    if(strlen($arr) <= $i) return 0;
    $num = (ord($arr[$i]) - 48) * 10;
    $num += ord($arr[$i + 1]) - 48;

    return sumPair($arr, $i + 2) + $num;
}

$arr = '1234567890';
echo sumPair($arr);