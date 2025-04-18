<?php
$path = "/web/";
$mainDir = opendir("/web/");
$dirs = [];

while ($dir = readdir($mainDir)) 
{
    if (is_dir("{$path}{$dir}") && $dir != '.' && $dir != '..')
    {
        $dirs[] = $dir;
    }
}
closedir($mainDir);

foreach ($dirs as $d)
{
    $dir = opendir("{$path}{$d}");
    
    while($file = readdir($dir))
    {
        if ($file == '.' || $file == '..') continue;

        if (is_file(filename: "{$path}{$d}/{$file}"))
        {
            echo "<tr>";

            $description = file_get_contents("{$path}{$d}/{$file}");
            echo "<td>{$d}</td>";
            echo "<td>{$file}</td>";
            echo "<td>{$description}</td>";

            echo "</tr>";
        }
    }
    closedir($dir);
}