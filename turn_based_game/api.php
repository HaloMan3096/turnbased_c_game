 <?php
	error_reporting(E_ALL);
	ini_set('display_errors', 1);
	$start = microtime(true);
	//Gets the UTC Date/Time at the time of this call
	$gmdate = gmdate("Y-m-d", time()); //Using UTC time

	require "func.php";
	$helper = new Helper();

	//ALL CALLS SHOULD RETURN THIS - ADD MORE PARAMETERS DEPENDING ON THE CALL
	$returnValue = array("success" => false, "serverTime" =>  stripslashes($gmdate));

    if(isset($_REQUEST['get-lobbies']))
	{		
		$returnValue["lobbies"] = $helper->GetLobbies();
		$returnValue["success"] = true;
	}	      
	elseif(isset($_REQUEST['test']))
	{
		$returnValue["success"] = true;
	}
	else
	{
		$returnValue["success"] = false;
		$returnValue["error"] = "Request not recognized";
		$returnValue["errorCode"] = 0;
	}

	if(isset($_REQUEST['pretty']))
	{
		//This is the line that puts text in the "response" that the client will receive;
		echo "<pre>";
		var_dump($returnValue);
		echo "</pre>";
	}
	else {
		echo '{ "response": ' . stripslashes(json_encode($returnValue)) . '}';
	}
?>
