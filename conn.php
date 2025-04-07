<?php

//Connect to the database
$link = new mysqli("localhost","root","", "turnbasedgame");

/* check connection */
if(mysqli_connect_errno())
{
	printf("Connect failed: %s\n", mysqli_connect_error());
	exit();
}

?>
