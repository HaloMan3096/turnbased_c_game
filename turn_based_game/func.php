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
			$lobby['id'] = $row['id'];
			$lobby['user1_id'] = $row['user1_id'];
			$lobby['user2_id'] = $row['user2_id'];
			
			array_push($lobbiesArr, $lobby);
		}
		
		$link->close();
                       
		return $lobbiesArr;
    }
}

?>
