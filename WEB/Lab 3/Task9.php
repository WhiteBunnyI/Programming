<?php
// ------------------------------- Task 9 -------------------------------

$massive = [];
$fill_with = 'x';
$fill_until = 5;

for ($i = 0; $i < $fill_until; $i++) 
{
    $temp = $fill_with;
    for ($j = 0; $j < $i; $j++)
    {
        $temp = "$temp$fill_with";
    }
    $massive[] = $temp;
}

print_r($massive);
echo "<br>";


// ------------------------------- Task 9 -------------------------------

function arrayFill($fill, $until)
{
    $massive = [];
    for ($i = 0; $i < $until; $i++) 
    {
        $massive[] = $fill;
    }
    return $massive;
}

print_r(arrayFill($fill_with, $fill_until));
echo "<br>";


// ------------------------------- Task 9 -------------------------------

function sumElements($m)
{
    $res = 0;
    foreach ($m as $k1 => $v1)
    {
        foreach ($v1 as $k2 => $v2)
        {
            $res += $v2;
        }
    }

    return $res;
}

$massive = [[1, 2, 3], [4, 5], [6]];
echo sumElements(@$massive);
echo "<br>";


// ------------------------------- Task 9 -------------------------------

$massive = [];
for($i = 0; $i < 3; $i++)
{
    $tempMassive = [];
    for($j = 0; $j < 3; $j++)
    {
        $tempMassive[] = $i * 3 + $j + 1;
    }
    $massive[] = $tempMassive;
}
print_r($massive);
echo "<br>";


// ------------------------------- Task 9 -------------------------------

$massive = [2, 5, 3, 9];
$result = $massive[0] * $massive[1] + $massive[2] * $massive[3];
echo "$result <br>";


// ------------------------------- Task 9 -------------------------------

$user = ["name" => "Oleg", "surname" => "Zubenko", "patronymic" => "Mafioznik"];
echo $user["surname"] . " " . $user["name"] . " " . $user["patronymic"] . "<br>";


// ------------------------------- Task 9 -------------------------------

$date = ["year" => 2025, "month" => 3, "day" => 21];
echo $date["year"] ."-". $date["month"] . "-" . $date["day"] . "<br>";


// ------------------------------- Task 9 -------------------------------

$arr = ['a', 'b', 'c', 'd', 'e'];
$count = count($arr);
echo "$count <br>";
echo $arr[$count - 1];
echo "<br>";
echo $arr[$count - 2];
echo "<br>";
