<?php

//Connect to the database
$link = new mysqli("localhost","Logan","12345", "turnbasedgame");

/* check connection */
if(mysqli_connect_errno())
{
	printf("Connect failed: %s\n", mysqli_connect_error());
	exit();
}

?>
