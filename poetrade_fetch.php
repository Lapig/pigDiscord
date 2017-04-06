<?php
  $ch = curl_init();
  $skipper = "luxury assault recreational vehicle";
  $fields = array( 'penguins'=>$skipper, 'memeteam'=>'fresharmy');
  $postvars = '';
  foreach($fields as $key=>$value) {
    $postvars .= $key . "=" . $value . "&";
  }													
  $url = "http://poe.trade/search/" . $argv[1];
  curl_setopt($ch,CURLOPT_URL,$url);//http://poe.trade/search/hunatononoitek shrieking suff
  curl_setopt($ch,CURLOPT_POST, 1);                //0 for a get request
  curl_setopt($ch,CURLOPT_POSTFIELDS,$postvars);
  curl_setopt($ch,CURLOPT_RETURNTRANSFER, true);
  curl_setopt($ch,CURLOPT_CONNECTTIMEOUT ,3);
  curl_setopt($ch,CURLOPT_TIMEOUT, 20);
  $response = curl_exec($ch);
  print "curl response is:" . $response;
  curl_close ($ch);
?>
