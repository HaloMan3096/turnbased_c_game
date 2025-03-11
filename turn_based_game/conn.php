<?php

//Connect to the database
$link = new mysqli("localhost","root","12345", "prog455");

/* check connection */
if(mysqli_connect_errno())
{
	printf("Connect failed: %s\n", mysqli_connect_error());
	exit();
}

?>
