var lib = {
	Speak: function(str) {
		var jsstr = Pointer_stringify(str);
		var msg = new SpeechSynthesisUtterance(jsstr);
		msg.lang = 'en-US';
		msg.volume = 1; 	// 0 to 1
		msg.rate = 1; 		// 0.1 to 10
		msg.pitch = 1; 		//0 to 2
		
		if (window.speechSynthesis.speaking)
		{
			window.speechSynthesis.cancel();
		}
		else
		{
			window.speechSynthesis.speak(msg);
		}
	},
	
	Exit: function() {
		window.history.back();
	},
	
	CloseTab: function() {
		window.close();
	}

};

mergeInto(LibraryManager.library, lib);