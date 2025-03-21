<?php
// ------------------------------- Task 6 -------------------------------

$a = 10;
$b = 3;
$rest = $a % $b;
if($rest == 0)
{
    echo "Делится " . $a / $b;
}
else
{
    echo "Делится с остатком: $rest";
}
echo "<br>";


// ------------------------------- Task 6 -------------------------------

$st = pow(2, 10);
$sq = sqrt(245);
echo "$st $sq<br>";


// ------------------------------- Task 6 -------------------------------

$massive = [4, 2, 5, 19, 13, 0, 10];
$result = 0;
foreach($massive as $k)
{
    $result += $k ** 2;
}
$result = sqrt($result);
echo "$result<br>";


// ------------------------------- Task 6 -------------------------------

$num = 379;
$num = sqrt($num);
$num1 = round($num, 0);
$num2 = round($num, 1);
$num3 = round($num, 2);
echo "$num1; $num2; $num3.<br>";

$num = 587;
$num = sqrt($num);
$massive = ["floor" => floor($num), "ceil" => ceil($num)];
print_r($massive);
echo "<br>";


// ------------------------------- Task 6 -------------------------------

$massive = [4, -2, 5, 19, -130, 0, 10];
echo "Макс: " . max($massive) . " | Мин: " . min($massive) . "<br>";


// ------------------------------- Task 6 -------------------------------

$random_num = rand(1, 100);
echo "$random_num <br>";
$massive = array();
for($i = 0; $i < 10; $i++)
{
    $massive[$i] = rand(1, 100);
}
print_r($massive);
echo "<br>";


// ------------------------------- Task 6 -------------------------------

$a = rand(1, 15);
$b = rand(5, 30);
$m1 = abs($a - $b);
$m2 = abs($b - $a);
echo "Числа: $a и $b <br>Модуль разности: $m1 | $m2 <br>";


$massive = [1, 2, -1, -2, 3, -3];
foreach($massive as $k => $v)
{
    $massive[$k] = abs($v);
}
print_r($massive);
echo "<br>";


// ------------------------------- Task 6 -------------------------------

$num = 30;
$massive = [];
for($i = 1; $i <= $num; $i++)
{
    if($num % $i == 0)
    {
        $massive[] = $i;
        echo "$i ";
    }
}
echo "<br>";

$massive = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
$sum = 0;
foreach($massive as $k => $v)
{
    $sum += $v;
    if($sum > 10)
    {
        echo "Необходимо сложить первые " . $k + 1 . " элементов <br>";
        break;
    }
}
