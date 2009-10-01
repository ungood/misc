chrome.extension.onConnect.addListener(function(port) {
	console.log('connect');
	if(port.name == 'changeTitle') {
		port.onMessage.addListener(function(msg) {
			console.log(msg);
			if(msg.title)
				document.title = msg.title;
		});	
	}
});