function t_animate__getAttrByRes(t,a){var e=$(window).width();return 1200<=e?t.attr("data-animate-"+a):"y"==t.attr("data-animate-mobile")?960<=e?t.attr("data-animate-"+a+"-res-960")||t.attr("data-animate-"+a):640<=e?t.attr("data-animate-"+a+"-res-640")||t.attr("data-animate-"+a+"-res-960")||t.attr("data-animate-"+a):480<=e?t.attr("data-animate-"+a+"-res-480")||t.attr("data-animate-"+a+"-res-640")||t.attr("data-animate-"+a+"-res-960")||t.attr("data-animate-"+a):320<=e?t.attr("data-animate-"+a+"-res-320")||t.attr("data-animate-"+a+"-res-480")||t.attr("data-animate-"+a+"-res-640")||t.attr("data-animate-"+a+"-res-960")||t.attr("data-animate-"+a):void 0:void t.css("transition","none")}function t_animate__init(){if(1==window.isSearchBot||"yes"==$(".t-records").attr("data-blocks-animationoff")||t_animate__checkIE()||"edit"==$(".t-records").attr("data-tilda-mode"))$(".t-animate").removeClass("t-animate");else{t_animate__wrapTextWithOpacity(),t_animate__addNoHoverClassToBtns(),0<$("[data-animate-style=fadeinleft]:not(.t396__elem)").length&&$(".t-records#allrecords").css("overflow-x","hidden");var t=$(".t-animate[data-animate-style='animatednumber']");1200<=$(window).width()&&t_animate__parseNumberText(t),setTimeout(function(){t_animate__startAnimation()},1500)}}function t_animate__checkMobile(t){return t.filter(function(){return"y"===$(this).attr("data-animate-mobile")||($(this).removeClass("t-animate"),!1)}),t}function t_animate__startAnimation(){var t=$(".r").has(".t-animate[data-animate-group=yes]"),a=$(".r").has(".t-animate[data-animate-chain=yes]"),e=$(".t-animate:not([data-animate-group=yes]):not([data-animate-chain=yes])");function n(){t_animate__getGroupsOffsets(t),t_animate__getChainOffsets(a),t_animate__getElemsOffsets(e)}$(window).width()<1200&&(t=t_animate__checkMobile(t),a=t_animate__checkMobile(a),e=t_animate__checkMobile(e)),(void 0!==t&&0!=t.length||void 0!==e&&0!=e.length||void 0!==a&&0!=a.length)&&(t_animate__setAnimationState(t,a,e),t=t.filter(".r:has(.t-animate_wait)"),e=e.filter(".t-animate_wait"),a=a.filter(".r:has(.t-animate_wait)"),n(),$(window).bind("resize",t_throttle(n,200)),setInterval(n,5e3),$(window).bind("scroll",t_throttle(function(){t_animate__animateOnScroll(t,a,e)},200)))}function t_animate__animateOnScroll(t,a,e){if(0!=t.length||0!=a.length||0!=e.length){var n=$(window).scrollTop(),i=n+$(window).height();if($("body").is(":animated")){for(var s=0;s<t.length;s++)t[s].curTopOffset<=n&&$(t[s]).find(".t-animate").removeClass("t-animate t-animate_wait t-animate_no-hover");for(s=0;s<e.length;s++)e[s].curTopOffset<=n&&$(e[s]).removeClass("t-animate t-animate_no-hover")}t_animate__animateGroups(t,i),t_animate__animateChainsBlocks(a,i),t_animate__animateElems(e,i)}}function t_animate__animateGroups(t,a){if(t.length)for(var e=0;e<t.length;e++)if(t[e].curTopOffset<a){var n=$(t[e]),i=n.find(".t-animate:not([data-animate-chain=yes])");t_animate__makeSectionButtonWait(n),i=i.filter(".t-animate:not(.t-animate__btn-wait-chain)"),t_animate__saveSectionHeaderStartTime(n),i.removeClass("t-animate_wait"),t_animate__removeNoHoverClassFromBtns(i),i.addClass("t-animate_started"),t.splice(e,1),e--}}function t_animate__animateChainsBlocks(t,a){for(var e=0;e<t.length;e++){var n=$(t[e]);t[e].itemsOffsets[0]>a||0==n.find(".t-animate_wait").length||(t_animate__animateChainItemsOnScroll(t,e,a),0==t[e].itemsOffsets.length&&(t.splice(e,1),e--),t_animate__checkSectionButtonAnimation__outOfTurn(n))}}function t_animate__animateChainItemsOnScroll(t,a,e){var n=$(t[a]),i=n.find(".t-animate_wait[data-animate-chain=yes]"),s=0,r=0,o=t[a].itemsOffsets[0],m=t_animate__getDelayFromPreviousScrollEvent(n,i,.16),_=t_animate__getSectionHeadDealy(n);$(i[0]).addClass("t-animate__chain_first-in-row");for(var l=0;l<i.length;l++){var d=$(i[l]),c=t[a].itemsOffsets[l];if(!(c<e))break;c!=o&&(d.addClass("t-animate__chain_first-in-row"),s=++r,o=c);var f=.16*s+m+_;d.css("transition-delay",f+"s"),d.removeClass("t-animate_wait"),d.addClass("t-animate_started"),d.attr("data-animate-start-time",Date.now()+1e3*f),d[0]==i.last()[0]&&t_animate__checkSectionButtonAnimation(n,f),c==o&&s++,i.splice(l,1),t[a].itemsOffsets.splice(l,1),l--}t_animate__catchTransitionEndEvent(n)}function t_animate__getSectionHeadDealy(t){var a=t.find(".t-section__title.t-animate"),e=t.find(".t-section__descr.t-animate"),n=0;return a.length&&Date.now()-a.attr("data-animate-start-time")<=160||e.length&&Date.now()-e.attr("data-animate-start-time")<=160?n=.16:n}function t_animate__getDelayFromPreviousScrollEvent(t,a,e){var n=0==t.find(".t-animate_started").length,i=t.find(".t-animate__chain_first-in-row.t-animate_started:not(.t-animate__chain_showed)");if(n||0==i.length)return 0;var s=i.last().attr("data-animate-start-time")-Date.now();return s<=0?e:s/1e3+ +e}function t_animate__catchTransitionEndEvent(t){t.find(".t-animate__chain_first-in-row.t-animate_started:not(.t-animate__chain_showed)").each(function(){$(this).on("TransitionEnd webkitTransitionEnd oTransitionEnd MSTransitionEnd",function(t){$(this).addClass("t-animate__chain_showed"),$(this).off(t)})})}function t_animate__animateElems(t,a){if(t.length)for(var e=0;e<t.length;e++){var n=t_animate__detectElemTriggerOffset($(t[e]),a);t[e].curTopOffset<n&&($(t[e]).removeClass("t-animate_wait"),t_animate__removeNoHoverClassFromBtns($(t[e])),$(t[e]).addClass("t-animate_started"),"animatednumber"==t_animate__getAttrByRes($(t[e]),"style")&&t_animate__animateNumbers($(t[e])),t.splice(e,1),e--)}}function t_animate__parseNumberText(t){var o=$(window).scrollTop();t.each(function(){var t=$(this),a="";if(0!==t.find('div[data-customstyle="yes"]').length){var e="";t.find("span").each(function(){e+=$(this).attr("style"),$(this).removeAttr("style"),$(this).removeAttr("data-redactor-style")}),a=t.find('div[data-customstyle="yes"]').html();var n=t.attr("style");n+=t.find("div[data-customstyle]").attr("style"),t.attr("style",n)}else{e="";t.find("span").each(function(){e+=$(this).attr("style"),$(this).removeAttr("style"),$(this).removeAttr("data-redactor-style")}),a=t.html()}if(!($(this).offset().top<o-500)&&0<a.length){var i=a.match(/\d+\.\d+|\d+\,\d+/g),s=a.replace(/(\d)(?= \d) /g,"$1"),r=[];s.split(" ").forEach(function(t,a){isNaN(parseInt(t))||r.push(t)}),r.forEach(function(t){var a=s.indexOf(t),e=s.substr(a,t.length);s=s.replace(e,"num")}),r&&(r.forEach(function(t,a){var e;if(null!==i&&(-1!==t.indexOf(",")&&(e=t.split(",")),-1!==t.indexOf(".")&&(e=t.split(".")),-1!==t.indexOf(",")||-1!==t.indexOf("."))){var n=e[1].length;r[a]=+e.join("."),r[a]=r[a].toFixed(n)}}),t.attr("data-animate-number-count",a),r.forEach(function(){t_animate__changeNumberOnZero(t,s)}),t.find("span:not(.t-animate__number):first").each(function(){$(this).attr("style",e)}))}})}function t_animate__changeNumberOnZero(t,a){var e;e=a.replace(/num/g,'<span class="t-animate__number">0</span>'),t.html(e)}function t_animate__animateNumbers(t){t.each(function(){var s=$(this),a=s.attr("data-animate-number-count"),e=[];if(s.find("span:not(.t-animate__number):first").each(function(){e=$(this).attr("style")}),a){var n=a.match(/\d+\.\d+|\d+\,\d+/g),r=a.match(/\d+/g),t=a.replace(/(\d)(?= \d) /g,"$1").split(" "),i=[];t.forEach(function(t,a){isNaN(parseInt(t))||i.push(t)});var o=0,m=!1;s.removeAttr("data-animate-number-count"),null!=n&&(m=-1!=n[0].indexOf(",")),i.forEach(function(t,a){var e;null!==n&&(-1!==t.indexOf(",")&&(e=t.split(",")),-1!==t.indexOf(".")&&(e=t.split(".")),-1===t.indexOf(",")&&-1===t.indexOf(".")||(o=e[1].length,i[a]=+e.join("."),0))});var _=[];s.find(".t-animate__number").each(function(){_.push($(this).text())}),i.forEach(function(t,i){$({animateCounter:_[i]}).animate({animateCounter:t},{duration:1500,easing:"swing",step:function(t){var a=s.find(".t-animate__number")[i],e=Math.pow(10,o),n=t?(Math.round(this.animateCounter*e)/e).toFixed(o)+"":Math.floor(this.animateCounter)+"";n=1<r.length?n.replace(/(\d)(?=(\d{3})+([^\d]|$))/g,"$1 "):n,m?$(a).text(n.replace(/\./g,",")):$(a).text(n)},complete:function(){s.html(a),s.find("span").each(function(){$(this).attr("style",e)})}})})}})}function t_animate__setAnimationState(t,a,e){var n=$(window).scrollTop(),i=n+$(window).height();t.each(function(){var t=$(this),a=t.find(".t-animate:not([data-animate-chain=yes])"),e=a.first().offset().top;if(t_animate__removeAnimFromHiddenSlides(t),t_animate__assignGroupDelay(t,t_animate__assignSectionDelay(t)),e<=n-100)return t_animate__saveSectionHeaderStartTime(t),a.removeClass("t-animate t-animate_no-hover"),a.css("transition-delay",""),!0;e<i&&n-100<e&&(t_animate__makeSectionButtonWait(t),t_animate__removeNoHoverClassFromBtns(a=a.filter(".t-animate:not(.t-animate__btn-wait-chain)")),a.addClass("t-animate_started")),i<=e&&a.addClass("t-animate_wait")}),a.each(function(){var t=$(this);t_animate__assignChainDelay(t,i,n),t_animate__checkSectionButtonAnimation__outOfTurn(t)}),e.each(function(){var t=$(this),a=t.offset().top;if(a<n-500)return t.removeClass("t-animate t-animate_no-hover"),"animatednumber"==t_animate__getAttrByRes(t,"style")&&t_animate__animateNumbers(t),!0;var e=t_animate__detectElemTriggerOffset(t,i);t_animate__setCustomAnimSettings(t,a,i),a<e&&(t_animate__removeNoHoverClassFromBtns(t),t.addClass("t-animate_started"),"animatednumber"==t_animate__getAttrByRes(t,"style")&&t_animate__animateNumbers(t)),e<=a&&t.addClass("t-animate_wait")}),$(window).bind("resize",t_throttle(t_animate__removeInlineAnimStyles,200))}function t_animate__assignSectionDelay(t){var a=0,e=t.find(".t-section__title.t-animate"),n=t.find(".t-section__descr.t-animate");t.find(".t-section__bottomwrapper .t-btn.t-animate");return e.length&&(a=.16),n.length&&(n.css("transition-delay",a+"s"),a+=.16),a}function t_animate__assignGroupDelay(t,a){var e=0;if(t.find("[data-animate-order]").length)t_animate__assignOrderedElemsDelay(t,a);else{var n=t.find(".t-img.t-animate"),i=t.find(".t-uptitle.t-animate"),s=t.find(".t-title.t-animate:not(.t-section__title)"),r=t.find(".t-descr.t-animate:not(.t-section__descr)"),o=t.find(".t-btn.t-animate:not(.t-section__bottomwrapper .t-btn)"),m=t.find(".t-timer.t-animate"),_=t.find("form.t-animate");0!=n.length&&(e=.5),0!=s.length&&s.css("transition-delay",e+"s"),0!=s.length&&(e+=.3),0!=r.length&&r.css("transition-delay",e+"s"),0!=r.length&&(e+=.3),0!=i.length&&i.css("transition-delay",e+"s"),0!=i.length&&(e+=.3),0==i.length&&0==s.length&&0==r.length||(e+=.2),0!=m.length&&m.css("transition-delay",e+"s"),0!=m.length&&(e+=.5),0!=o.length&&$(o.get(0)).css("transition-delay",e+"s"),2==o.length&&(e+=.4),2==o.length&&$(o.get(1)).css("transition-delay",e+"s"),0!=_.length&&_.css("transition-delay",e+"s")}}function t_animate__assignOrderedElemsDelay(t,a){var e=0;void 0!==a&&a&&(e=a);var n=t.find(".t-animate[data-animate-order=1]"),i=t.find(".t-animate[data-animate-order=2]"),s=t.find(".t-animate[data-animate-order=3]"),r=t.find(".t-animate[data-animate-order=4]"),o=t.find(".t-animate[data-animate-order=5]");elem6=t.find(".t-animate[data-animate-order=6]"),elem7=t.find(".t-animate[data-animate-order=7]"),elem8=t.find(".t-animate[data-animate-order=8]"),elem9=t.find(".t-animate[data-animate-order=9]"),n.length&&n.css("transition-delay",e+"s"),n.length&&i.length&&(e+=+t_animate__getAttrByRes(i,"delay"),i.css("transition-delay",e+"s")),(n.length||i.length)&&s.length&&(e+=+t_animate__getAttrByRes(s,"delay"),s.css("transition-delay",e+"s")),(n.length||i.length||s.length)&&r.length&&(e+=+t_animate__getAttrByRes(r,"delay"),r.css("transition-delay",e+"s")),(n.length||i.length||s.length||r.length)&&o.length&&(e+=+t_animate__getAttrByRes(o,"delay"),o.css("transition-delay",e+"s")),(n.length||i.length||s.length||r.length||o.length)&&elem6.length&&(e+=+t_animate__getAttrByRes(elem6,"delay"),elem6.css("transition-delay",e+"s")),(n.length||i.length||s.length||r.length||o.length||elem6.length)&&elem7.length&&(e+=+t_animate__getAttrByRes(elem7,"delay"),elem7.css("transition-delay",e+"s")),(n.length||i.length||s.length||r.length||o.length||elem6.length||elem7.length)&&elem8.length&&(e+=+t_animate__getAttrByRes(elem8,"delay"),elem8.css("transition-delay",e+"s")),(n.length||i.length||s.length||r.length||o.length||elem6.length||elem7.length||elem8.length)&&elem9.length&&(e+=+t_animate__getAttrByRes(elem9,"delay"),elem9.css("transition-delay",e+"s"))}function t_animate__assignChainDelay(n,i,s){var r=n.find(".t-animate[data-animate-chain=yes]"),o=0;if(0!=r.length){var m=$(r[0]).offset().top;$(r[0]).addClass("t-animate__chain_first-in-row");var _=t_animate__getCurBlockSectionHeadDelay(n);r.each(function(){var t=$(this),a=t.offset().top;if(a<s)return t.removeClass("t-animate"),!0;if(a<i){a!=m&&(t.addClass("t-animate__chain_first-in-row"),m=a);var e=.16*o+_;t.css("transition-delay",e+"s"),t.addClass("t-animate_started"),t.attr("data-animate-start-time",Date.now()+1e3*e),t[0]==r.last()[0]&&t_animate__checkSectionButtonAnimation(n,e),o++,t.on("TransitionEnd webkitTransitionEnd oTransitionEnd MSTransitionEnd",function(t){$(this).addClass("t-animate__chain_showed"),$(this).off(t)})}else t.addClass("t-animate_wait")})}}function t_animate__setCustomAnimSettings(t,a,e){var n=t_animate__getAttrByRes(t,"style"),i=t_animate__getAttrByRes(t,"distance");void 0!==i&&""!=i&&(i=i.replace("px",""),t.css({"transition-duration":"0s","transition-delay":"0s"}),"fadeinup"==n&&t.css("transform","translate3d(0,"+i+"px,0)"),"fadeindown"==n&&t.css("transform","translate3d(0,-"+i+"px,0)"),"fadeinleft"==n&&t.css("transform","translate3d("+i+"px,0,0)"),"fadeinright"==n&&t.css("transform","translate3d(-"+i+"px,0,0)"),t_animate__forceElemInViewPortRepaint(t,a,e),t.css({"transition-duration":"","transition-delay":""}));var s=t_animate__getAttrByRes(t,"scale");void 0!==s&&""!=s&&(t.css({"transition-duration":"0s","transition-delay":"0s"}),t.css("transform","scale("+s+")"),t_animate__forceElemInViewPortRepaint(t,a,e),t.css({"transition-duration":"","transition-delay":""}));var r=t_animate__getAttrByRes(t,"delay");void 0!==r&&""!=r&&t.css("transition-delay",r+"s");var o=t_animate__getAttrByRes(t,"duration");void 0!==o&&""!=o&&t.css("transition-duration",o+"s")}function t_animate__removeInlineAnimStyles(){$(window).width()<980&&$(".t396__elem.t-animate").css({transform:"","transition-duration":"","transition-delay":""})}function t_animate__forceElemInViewPortRepaint(t,a,e){a<e+500&&t[0].offsetHeight}function t_animate__detectElemTriggerOffset(t,a){var e=t_animate__getAttrByRes(t,"trigger-offset"),n=a;return void 0!==e&&""!=e&&(n=a-(e=e.replace("px",""))),n}function t_animate__saveSectionHeaderStartTime(t){var a=t.find(".t-section__title.t-animate"),e=t.find(".t-section__descr.t-animate");a.length&&a.attr("data-animate-start-time",Date.now()),e.length&&e.attr("data-animate-start-time",Date.now()+160)}function t_animate__getCurBlockSectionHeadDelay(t){var a=0;return 0!=t.find(".t-section__title.t-animate").length&&(a+=.16),0!=t.find(".t-section__descr.t-animate").length&&(a+=.16),a}function t_animate__makeSectionButtonWait(t){var a=t.find(".t-animate[data-animate-chain=yes]").length,e=t.find(".t-section__bottomwrapper .t-btn.t-animate");a&&e&&e.addClass("t-animate__btn-wait-chain")}function t_animate__checkSectionButtonAnimation(t,a){var e=t.find(".t-animate__btn-wait-chain");e.length&&(e.css("transition-delay",a+.16+"s"),t_animate__removeNoHoverClassFromBtns(e),e.removeClass("t-animate__btn-wait-chain"),e.addClass("t-animate_started"))}function t_animate__checkSectionButtonAnimation__outOfTurn(t){if(0===t.find(".t-animate[data-animate-chain=yes]:not(.t-animate_started)").length){t_animate__checkSectionButtonAnimation(t,.16)}}function t_animate__addNoHoverClassToBtns(){$(".t-btn.t-animate").addClass("t-animate_no-hover")}function t_animate__removeNoHoverClassFromBtns(t){t.filter(".t-btn").each(function(){$(this).get(0).addEventListener("transitionend",function(t){"opacity"!=t.propertyName&&"transform"!=t.propertyName||($(this).removeClass("t-animate_no-hover"),$(this).css("transition-delay",""),$(this).css("transition-duration",""),$(this).off(t))})})}function t_animate__getGroupsOffsets(t){for(var a=0;a<t.length;a++)0<$(t[a]).find(".t-animate").length&&(t[a].curTopOffset=$(t[a].querySelector(".t-animate")).offset().top)}function t_animate__getChainOffsets(t){for(var a=0;a<t.length;a++){var e=$(t[a]).find(".t-animate_wait[data-animate-chain=yes]");t[a].itemsOffsets=[];for(var n=0;n<e.length;n++)0<$(e[n]).length&&(t[a].itemsOffsets[n]=$(e[n]).offset().top)}}function t_animate__getElemsOffsets(t){for(var a=0;a<t.length;a++)t[a].curTopOffset=$(t[a]).offset().top}function t_animate__removeAnimFromHiddenSlides(t){t.find(".t-slides").length&&t.find(".t-slides__item:not(.t-slides__item_active) .t-animate").removeClass("t-animate t-animate_no-hover")}function t_animate__wrapTextWithOpacity(){$(".t-title.t-animate,.t-descr.t-animate,.t-uptitle.t-animate,.t-text.t-animate").each(function(){var t=$(this),a=$(this).attr("style");if(void 0!==a&&0<=a.indexOf("opacity")){var e=a.indexOf("opacity");if(0<=a.slice(e).indexOf(";"))var n=a.slice(e,e+a.slice(e).indexOf(";"));else n=a.slice(e);var i='<div style="'+n+';">'+t.html()+"</div>";t.css("opacity",""),t.html(i)}})}function t_animate__checkIE(){var t=window.navigator.userAgent,a=t.indexOf("MSIE"),e="",n=!1;return 0<a&&(8!=(e=parseInt(t.substring(a+5,t.indexOf(".",a))))&&9!=e||(n=!0)),n}$(document).ready(function(){t_animate__init()});