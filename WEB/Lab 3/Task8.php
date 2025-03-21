<?php
// ------------------------------- Task 8 -------------------------------

function increaseEnthusiasm($str)
{
    return "$str!";
}

function repeatThreeTimes($str)
{
    return "$str$str$str";
}

function cut($str, $length = 10)
{
    $result = "";
    $str_len = strlen($str);

    //2 т.к. русские буквы занимают 2 байта
    for ($i = 0; $i < $length; $i++)
    {
        if($i >= $str_len) break;
        $result .= $str[$i];    
    }

    return $result;
}

function printMassive($m, $i = 0)
{
    if($i >= count($m)) return "";
    echo $m[$i] . " ";
    printMassive($m, $i + 1);
    echo "<br>";
}

function sumDigits($num)
{
    $result = 0;
    while($num > 0)
    {
        $result += $num % 10;
        $num = floor($num / 10);
        if($num == 0 and $result > 9)
        {
            $num = $result;
            $result = 0;
        }
    }
    return $result;
}

echo increaseEnthusiasm("Ура") . "<br>";
echo repeatThreeTimes("Ура") . "<br>";
echo increaseEnthusiasm(repeatThreeTimes("Ура"));
echo "<br>";
echo cut("Some long text for test");
echo "<br>";
echo cut("Some long text for test", 20);
echo "<br>";

$massive = [1,2,3,4,5,6];
echo printMassive($massive);

$num = 12345;
echo sumDigits($num);