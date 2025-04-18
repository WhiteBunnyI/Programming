<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Krutaya doska</title>
</head>
<body>
    <div id="form">
        <form action="save.php" method="POST">
            <label for="email">Email</label>
            <input type="email" name="email" required>

            <label for="category">Category</label>
            <select name="category" required>
            <?php
                $dir = opendir("/web/");
                while ($file = readdir($dir)) {
                    if (is_dir("/web/{$file}") && $file != '.' && $file != '..')
                    {
                        echo "<option value='{$file}'>{$file}</option>";
                    }
                }
            ?>
            </select>

            <label for="title">Title</label>
            <input type="text" name="title" required>

            <label for="description">Description</label>
            <textarea rows="3" name="description"></textarea>

            <button type="submit">Save</button>
        </form>
    </div>

    <div id="table">
        <table>
            <thead>
                <th>Category</th>
                <th>Title</th>
                <th>Description</th>
            </thead>
            <tbody>
                <?php require "load.php" ?>
            </tbody>
        </table>
    </div>
</body>
</html>