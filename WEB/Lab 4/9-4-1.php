<?php
    session_start();
    $saved = false;
    if(isset($_POST["name"]))
    {
        $_SESSION["name"] = $_POST["name"];
        $saved = true;
    }
    if(isset($_POST["author"]))
    {
        $_SESSION["author"] = $_POST["author"];
        $saved = true;
    }
    if(isset($_POST["date"]))
    {
        $_SESSION["date"] = $_POST["date"];
        $saved = true;
    }

    if($saved)
    {
        echo "Данные сохранены!";
    }
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    
    
    <form method="POST">
        <input type="text" name="name" placeholder="Название книги">
        <input type="text" name="author" placeholder="Автор">
        <input type="date" name="date" placeholder="Год издания">
        <input type="submit" value="Отправить">
    </form>

    <a href="9-4-2.php">Другая страничка</a>
</body>
</html>