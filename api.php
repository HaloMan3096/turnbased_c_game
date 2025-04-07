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
	elseif(isset($_REQUEST['join-lobby']))
	{
		$uID = $_REQUEST['user-id'];
		$lID = $_REQUEST['lobby-id'];

		$helper->JoinLobby($uID,$lID);
		$helper->StartGame($lID);

		$returnValue["success"] = true;
	}  
	elseif(isset($_REQUEST['take-turn']))
	{
		$gID = $_REQUEST['game-id'];
		$answer = $_REQUEST['answer'];

		$helper->TakeTurn($gID,$answer);
		$returnValue["success"] = true;
	}  
	elseif(isset($_REQUEST['check-game-status']))
	{
		$gID = $_REQUEST['game-id'];

		$returnValue["gameData"] = $helper->CheckGameStatus($gID);
		$returnValue["success"] = true;
	}  
	elseif(isset($_REQUEST['create-lobby']))
	{
		$uID = $_REQUEST['user-id'];

		$returnValue["lID"]		= $helper->CreateLobby($uID);
		$returnValue["success"] = true;
	}
	elseif(isset($_REQUEST['exit-lobby']))
	{
		$uID = $_REQUEST['user-id'];
		$lID = $_REQUEST['lobby-id'];

		$helper->ExitLobby($uID,$lID);
		$returnValue["success"] = true;
	} 
	elseif(isset($_REQUEST['test']))
	{
		$returnValue["success"] = true;
	}
	else
	{
		$returnValue["success"] 	= false;
		$returnValue["error"] 		= "Request not recognized";
		$returnValue["errorCode"] 	= 0;
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
