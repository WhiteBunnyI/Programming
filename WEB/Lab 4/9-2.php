<?php

function callback_function($match)
{
    return intval($match[0]) * 4;
}

$text = "a1b22c3";
$regexp = "/[0-9]+/i";

echo preg_replace_callback($regexp,"callback_function", $text);

