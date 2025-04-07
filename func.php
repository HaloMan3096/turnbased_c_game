<?php

class Helper
{
    public function GetLobbies()
    {
        require 'conn.php';
        
		set_time_limit(0);			
        
        $sql = "SELECT * FROM lobby;";
        $result = mysqli_query($link, $sql) or die($this->MysqlErrorMessage($link));
		
		$lobbiesArr = Array();
        while($row = mysqli_fetch_assoc($result))
		{
			$lobby = Array();
			$lobby['id'] = (int)$row['id'];
			$lobby['user1_id'] = (int)$row['user1_id'];
			$lobby['user2_id'] = (int)$row['user2_id'];
			
			array_push($lobbiesArr, $lobby);
		}
		
		$link->close();
                       
		return $lobbiesArr;
	}
	
	public function JoinLobby($uID,$lID)
	{
		require 'conn.php';
        
		set_time_limit(0);					

        $sql = "UPDATE lobby set user2_id=$uID where id = $lID;";
        mysqli_query($link, $sql) or die($this->MysqlErrorMessage($link));

		$link->close();
	}

	public function StartGame($lID)
	{
		require 'conn.php';
        
		set_time_limit(0);					

		//look up players involved
		$sql = "SELECT user1_id,user2_id FROM lobby WHERE id=$lID;";
        $result = mysqli_query($link, $sql) or die($this->MysqlErrorMessage($link));

		$turnID = 0;
		//pick player 1 or player 2 to be the first player to go
		while($row = mysqli_fetch_assoc($result))
		{
			$turnID = rand(0,100) > 50 ? $row['user1_id'] : $row['user2_id'];
		}
		//pick a number 1-10 as the answer for the game
		$gameAnswer = rand(1,10);

        $sql = "INSERT INTO game (id,turn_id,answer) values($uID,$turnID,$gameAnswer);";
        mysqli_query($link, $sql) or die($this->MysqlErrorMessage($link));

		$link->close();
	}
	
	public function TakeTurn($gID,$answer)
	{
		require 'conn.php';
        
		set_time_limit(0);					

		//look up players involved
		$sql = "SELECT turn_id,answer FROM game WHERE id=$gID;";
        $result = mysqli_query($link, $sql) or die($this->MysqlErrorMessage($link));

		$userID = 0;
		$correctAnswer = 0;

		while($row = mysqli_fetch_assoc($result))
		{
			$correctAnswer = $row['answer'];
			$userID = $row['turn_id'];
		}
		
		if($answer == $correctAnswer)
		{
			//update the winner in the game
			$sql = "UPDATE game SET winner_id=$userID,active=0 WHERE id=$gID;";
        	mysqli_query($link, $sql) or die($this->MysqlErrorMessage($link));
		}
		else 
		{
			$sql = "SELECT user1_id,user2_id FROM lobby WHERE id=$gID;";
			$result = mysqli_query($link, $sql) or die($this->MysqlErrorMessage($link));
			$next = 0;

			while($row = mysqli_fetch_assoc($result))
			{
				$next = $userID == $row['user1_id'] ? $row['user2_id'] : $row['user1_id'];
			}

			$sql = "UPDATE game SET turn_id=$next WHERE id=$gID;";
        	mysqli_query($link, $sql) or die($this->MysqlErrorMessage($link));
		}        

		$link->close();
	}

	public function CheckGameStatus($gID)
    {
        require 'conn.php';
        
		set_time_limit(0);			
        
        $sql = "SELECT turn_id,winner_id,active FROM game WHERE id=$gID;";
        $result = mysqli_query($link, $sql) or die($this->MysqlErrorMessage($link));
		
		$gameState = Array();

        while($row = mysqli_fetch_assoc($result))
		{
			$gameState['turn_id'] = (int)$row['turn_id'];
			$gameState['winner_id'] = (int)$row['winner_id'];	
			$gameState['active'] = (int)$row['active'] == 1;			
		}
		
		$link->close();
                       
		return $gameState;
	}

	public function CreateLobby($uID)
	{
		require 'conn.php';
        
		set_time_limit(0);					
		
		$sql = "INSERT INTO lobby (user1_id) values($uID);";
		$result = mysqli_query($link, $sql) or die($this->MysqlErrorMessage($link));

		$sql = "SELECT id FROM lobby WHERE user1_id=$uID;";
		$result = mysqli_query($link, $sql) or die($this->MysqlErrorMessage($link));

		$lID = -1;

		while($row = mysqli_fetch_assoc($result))
		{
			$lID = (int)$row['id'];
		}	

		$link->close();

		return $lID;
	}

	public function ExitLobby($uID,$lID)
	{
		require 'conn.php';
        
		set_time_limit(0);					

		$sql = "UPDATE lobby SET user1_id = NULL WHERE id = $lID AND user1_id = $uID;";
		$result = mysqli_query($link, $sql) or die($this->MysqlErrorMessage($link));

		$sql = "UPDATE lobby SET user2_id = NULL WHERE id = $lID AND user2_id = $uID;";
		$result = mysqli_query($link, $sql) or die($this->MysqlErrorMessage($link));

		$sql = "DELETE FROM lobby WHERE user1_id IS NULL AND user2_id IS NULL;";
		$result = mysqli_query($link, $sql) or die($this->MysqlErrorMessage($link));

		$link->close();
	}
}

?>
