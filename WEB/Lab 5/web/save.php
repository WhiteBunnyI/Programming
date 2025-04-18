<?php

if(isset($_POST["email"], $_POST["category"], $_POST["title"], $_POST["description"]))
{
    $email = $_POST["email"];
    $category = $_POST["category"];
    $title = $_POST["title"];
    $description = $_POST["description"];

    $filePath = "/web/{$category}/{$title}";

    if (file_put_contents($filePath, $description) === false)
    {
        throw new Exception("Ошибка: не удалось записать файл");
    }
}

header("Location: /");
exit();