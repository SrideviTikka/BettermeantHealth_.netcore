/*!
 * classie - class helper functions
 * classie.has( elem, 'my-class' ) -> true/false
 * classie.add( elem, 'my-new-class' )
 * classie.remove( elem, 'my-unwanted-class' )
 * classie.toggle( elem, 'my-class' )
 */

/*jshint browser: true, strict: true, undef: true */
/*global define: false */

(function (window) {

    'use strict';

    function classReg(className) {
        return new RegExp("(^|\\s+)" + className + "(\\s+|$)");
    }

    // classList support for class management
    // altho to be fair, the api sucks because it won't accept multiple classes at once
    var hasClass, addClass, removeClass;

    if ('classList' in document.documentElement) {
        hasClass = function (elem, c) {
            return elem.classList.contains(c);
        };
        addClass = function (elem, c) {
            elem.classList.add(c);
        };
        removeClass = function (elem, c) {
            elem.classList.remove(c);
        };
    }
    else {
        hasClass = function (elem, c) {
            return classReg(c).test(elem.className);
        };
        addClass = function (elem, c) {
            if (!hasClass(elem, c)) {
                elem.className = elem.className + ' ' + c;
            }
        };
        removeClass = function (elem, c) {
            elem.className = elem.className.replace(classReg(c), ' ');
        };
    }

    function toggleClass(elem, c) {
        var fn = hasClass(elem, c) ? removeClass : addClass;
        fn(elem, c);
    }
    var classie = {
        // full names
        hasClass: hasClass,
        addClass: addClass,
        removeClass: removeClass,
        toggleClass: toggleClass,
        // short names
        has: hasClass,
        add: addClass,
        remove: removeClass,
        toggle: toggleClass
    };

    // transport
    if (typeof define === 'function' && define.amd) {
        // AMD
        define(classie);
    } else {
        // browser global
        window.classie = classie;
    }

})(window);

(function () {

    var container = document.getElementById('switcher'),
		optionSwitch = Array.prototype.slice.call( container.querySelectorAll( 'div.msb-options > a' ) );

	function init() {
		optionSwitch.forEach( function( el, i ) {
			el.addEventListener( 'click', function( ev ) {
				ev.preventDefault();
				_switch( this );
			}, false );
		} );
	}

	function _switch(opt) {
	    if (opt.className.indexOf("msb-kendo") <= -1) {
	        $(container).find(".k-widget.k-listview").show();
	        $(container).find(".k-widget.k-gridview").hide();
	        $(container).find(".k-grid.k-widget").hide();
	        // remove other view classes and any any selected option
	        optionSwitch.forEach(function (el) {
	            classie.remove(container, el.getAttribute('data-view'));
	            classie.remove(el, 'msb-selected');
	        });
	        // add the view class for this option
	        classie.add(container, opt.getAttribute('data-view'));
	    }
	    else {
            // if it is kendo
	        optionSwitch.forEach(function (el) {
	            $(container).find(".k-widget.k-listview").hide();
	            $(container).find(".k-widget.k-gridview").show();
	            $(container).find(".k-grid.k-widget").show();
	            classie.remove(container, el.getAttribute('data-view'));
	            classie.remove(el, 'msb-selected');
	        });
	    }
                   
		// this option stays selected
		classie.add( opt, 'msb-selected' );
	}

	init();

})();