export class GlobalChatIcon {
	static main() {
		const _COOKIE_NAME = 'ST_chat_dismissed';
		const cookie = getCookie(_COOKIE_NAME);
		const windowLocation = document.location.pathname;

		if (windowLocation === '/') {
			if (jQuery.isEmptyObject(cookie)) {
				$('.global-chat').toggleClass('open');
			}
		}

		$(document).on('click', '.global-chat .chat-icon, .global-chat .close-btn', function(e) {
			e.preventDefault();
			$('.global-chat').toggleClass('open');
			setCookie(_COOKIE_NAME, 'true', 365);
		});

		/**
		 * All params required; defaults to secure cookie;
		 * 
		 * @param {String} name 
		 * @param {String} value 
		 * @param {Number} valid 
		 */
		function setCookie(name, value, valid) {
			let date = new Date();
			date.setTime(date.getTime() + (valid*24*60*60*1000));
			const expires = "expires="+ date.toUTCString();
			document.cookie = name + "=" + value + ";" + expires + "; secure; path=/";
		}

		/**
		 * Name param required; returns cookie object
		 * { key, value }
		 * 
		 * @param {String} name 
		 * @return {Object}
		 */
		function getCookie(name) {
			const decodedCookie = decodeURIComponent(document.cookie);
			const cookies = decodedCookie.split(';');
			let cookieObject = {};
			cookies.forEach(cookie => {
				if(cookie.includes(name)) {
					cookie = cookie.split('=');
					cookie = {
						key: cookie[0],
						value: cookie[1],
					};
					cookieObject = cookie;
				}
			});

			return cookieObject;
		}

	}
}