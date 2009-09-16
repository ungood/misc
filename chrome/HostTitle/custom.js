function changeTitle() {
 	var protoIndex = document.URL.indexOf("://");
	if(protoIndex < 0)
		return;
		
	var domainIndex = document.URL.indexOf("/", protoIndex + 3);
	if(domainIndex < 0)
		return;
	
 	document.title= document.title + ' [' + document.URL.slice(protoIndex + 3, domainIndex) + ']';
}

setTimeout(changeTitle, 100);