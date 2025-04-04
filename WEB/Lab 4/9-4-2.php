<?php

session_start();

$name = "";
$author = "";
$date = "";

if(isset($_SESSION["name"]))
{
    $name = $_SESSION["name"];
}
if(isset($_SESSION["author"]))
{
    $author = $_SESSION["author"];
}
if(isset($_SESSION["date"]))
{
    $date = $_SESSION["date"];
}

echo "Название книги: " . $name . "<br>";
echo "Автор книги: " . $author . "<br>";
echo "Год издания: " . $date . "<br>";