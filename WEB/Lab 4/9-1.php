<?php

$text = "abdIgti vasd issa idfi iasdfai vxciai ibgfi i12i ifdgi iiii jfidgi";
$regexp = "/i\w\wi/i";
$matches = [];

$count = preg_match_all($regexp, $text, $matches, PREG_OFFSET_CAPTURE);

for ($i = 0; $i < $count; $i++) 
{
    echo "Позиция: " . $matches[0][$i][1] . " | подстрока: " . $matches[0][$i][0] . "<br>";
}