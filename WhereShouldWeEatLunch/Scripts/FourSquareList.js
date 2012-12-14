﻿function loadFourSquareResults(n){showWaiting(),$.getJSON("/eatery/FourSquareListByCoords",{lat:lat,long:lon,categoryId:n},function(n){var i=[],t;$("#eateryList").html="",t='<li id="{0}"><a href="http://maps.google.com/?daddr={2},{3}">{1}</a> ({4} mi.)</li>',$.each(n,function(n,r){i.push(t.format(n,r.name,r.location.lat,r.location.lng,r.Distance.toFixed(2)))}),$("#eateryList").html(i.join(""))})}function success_callback(n){lat=n.coords.latitude,lon=n.coords.longitude,loadFourSquareResults()}function showWaiting(){$("#eateryList").html("<li>Reticulating Splines.... Please Wait</li>")}function error_callback(n){alert("error="+n.message)}function handleBlackBerryLocationTimeout(){bb_blackberryTimeout_id!=-1&&bb_error({message:"Timeout error",code:3})}function handleBlackBerryLocation(){if(clearTimeout(bb_blackberryTimeout_id),bb_blackberryTimeout_id=-1,bb_success&&bb_error){if(blackberry.location.latitude==0&&blackberry.location.longitude==0)bb_error({message:"Position unavailable",code:2});else{var n=null;blackberry.location.timestamp&&(n=new Date(blackberry.location.timestamp)),bb_success({timestamp:n,coords:{latitude:blackberry.location.latitude,longitude:blackberry.location.longitude}})}bb_success=null,bb_error=null}}var lat,lon,bb_success,bb_error,bb_blackberryTimeout_id,geo_position_js;geo_position_js.init()?geo_position_js.getCurrentPosition(success_callback,error_callback,{enableHighAccuracy:!0}):alert("Functionality not available"),$(document).ready(function(){$("#foodStyle").change(function(){var n=$(this).val().join().trim();n.indexOf("0,")==0&&(n=n.substring(2,n.length)),loadFourSquareResults(n)})}),String.prototype.format=function(){var n=arguments;return this.replace(/{(\d+)}/g,function(t,i){return typeof n[i]!="undefined"?n[i]:t})},function(){if(!window.google||!google.gears){var n=null;if(typeof GearsFactory!="undefined")n=new GearsFactory;else try{n=new ActiveXObject("Gears.Factory"),n.getBuildInfo().indexOf("ie_mobile")!=-1&&n.privateSetGlobalObject(this)}catch(t){typeof navigator.mimeTypes!="undefined"&&navigator.mimeTypes["application/x-googlegears"]&&(n=document.createElement("object"),n.style.display="none",n.width=0,n.height=0,n.type="application/x-googlegears",document.documentElement.appendChild(n),n&&typeof n.create=="undefined"&&(n=null))}n&&(window.google||(google={}),google.gears||(google.gears={factory:n}))}}(),bb_blackberryTimeout_id=-1,geo_position_js=function(){var i={},t=null,n="undefined";return i.getCurrentPosition=function(n,i,r){t.getCurrentPosition(n,i,r)},i.init=function(){try{if(typeof geo_position_js_simulator!=n)t=geo_position_js_simulator;else if(typeof bondi!=n&&typeof bondi.geolocation!=n)t=bondi.geolocation;else if(typeof navigator.geolocation!=n)t=navigator.geolocation,i.getCurrentPosition=function(i,r,u){function f(t){typeof t.latitude!=n?i({timestamp:t.timestamp,coords:{latitude:t.latitude,longitude:t.longitude}}):i(t)}t.getCurrentPosition(f,r,u)};else if(typeof window.blackberry!=n&&blackberry.location.GPSSupported){if(typeof blackberry.location.setAidMode==n)return!1;blackberry.location.setAidMode(2),i.getCurrentPosition=function(n,t,i){bb_success=n,bb_error=t,bb_blackberryTimeout_id=i.timeout?setTimeout("handleBlackBerryLocationTimeout()",i.timeout):setTimeout("handleBlackBerryLocationTimeout()",6e4);blackberry.location.onLocationUpdate("handleBlackBerryLocation()");blackberry.location.refreshLocation()},t=blackberry.location}else typeof window.google!=n&&typeof google.gears!=n?t=google.gears.factory.create("beta.geolocation"):typeof Mojo!=n&&typeof Mojo.Service.Request!="Mojo.Service.Request"?(t=!0,i.getCurrentPosition=function(n,t,i){parameters={},i&&(i.enableHighAccuracy&&i.enableHighAccuracy==!0&&(parameters.accuracy=1),i.maximumAge&&(parameters.maximumAge=i.maximumAge),i.responseTime&&(i.responseTime<5?parameters.responseTime=1:i.responseTime<20?parameters.responseTime=2:parameters.timeout=3)),r=new Mojo.Service.Request("palm://com.palm.location",{method:"getCurrentPosition",parameters:parameters,onSuccess:function(t){n({timestamp:t.timestamp,coords:{latitude:t.latitude,longitude:t.longitude,heading:t.heading}})},onFailure:function(n){n.errorCode==1?t({code:3,message:"Timeout"}):n.errorCode==2?t({code:2,message:"Position Unavailable"}):t({code:0,message:"Unknown Error: webOS-code"+errorCode})}})}):typeof device!=n&&typeof device.getServiceObject!=n&&(t=device.getServiceObject("Service.Location","ILocation"),i.getCurrentPosition=function(n,i){function f(t,r,u){r==4?i({message:"Position unavailable",code:2}):n({timestamp:null,coords:{latitude:u.ReturnValue.Latitude,longitude:u.ReturnValue.Longitude,altitude:u.ReturnValue.Altitude,heading:u.ReturnValue.Heading}})}var u={};u.LocationInformationClass="BasicLocationInformation",t.ILocation.GetLocation(u,f)})}catch(u){return typeof console!=n&&console.log(u),!1}return t!=null},i}()