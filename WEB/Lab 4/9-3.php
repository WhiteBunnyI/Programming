<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    <?php
        $text = "";
        if(isset($_POST["textarea"])){
  
            $text = $_POST["textarea"];
        }
        $regexp = "/\b[aeiou]\w*\b/i";
        $count = preg_match_all( $regexp, $text );
        echo "Длина текста: " . strlen($text) . "<br>";
        echo "Кол-во найденных слов с гласными в начале: " . $count;
    ?>
    
    <form method="post">
        <input type="text" name="textarea">
        <input type="submit" value="Отправить">
    </form>
</body>
</html>