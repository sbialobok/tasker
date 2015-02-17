/**
 * @jsx React.DOM
 */
"use strict";

var React = require('react');

var baseTestHelper = function ($) {
    var render = function() {
        return (
            <div/>
        );
    };
    return {
        spy: {
            mocks: [],
            jqmocks: [],
            onGlobalObject: function(name, functions) {
                //Will mock a global object without having to worry about specifics

                var obj = window[name],
                    mock = null;

                //if we need to mock function create a spy object and push the actual object onto our array
                if(functions) {
                    mock = jasmine.createSpyObj(name, functions);
                } else if(obj) {
                    //if no functions are needed, just spyon the object itself. no need to push object 
                    //onto array because spy will be removed once object leaves scope of unit test
                    mock = spyOn(window, name);
                } else {
                    //no object or functions, just create a basic spy
                    mock = jasmine.createSpy(name);
                }
                
                //push info onto array.  Will be popped off and set to window[varName] = obj in global after each
                this.mocks.push({
                    varName: name,
                    obj: obj
                });

                window[name] = mock;

                return mock;
            },
            onjQueryPlugin: function(name) {
                //Will mock global jquery plugin without having to worry about specifics
                
                var obj = $.fn[name],
                    mock = null;

                if(obj) {
                    mock = spyOn($.fn, name);
                } else {
                    mock = jasmine.createSpy(name);
                }

                this.jqmocks.push({
                    varName: name,
                    obj: obj
                });

                $.fn[name] = mock;

                return mock;
            },
            onReactComponent: function(rewiredModule, componentName) {
                var mockComponent = React.createClass({
                    render: render
                });

                spyOn(mockComponent.type.prototype, 'render').and.callFake(render);
                rewiredModule.__set__(componentName, mockComponent);

                return mockComponent;
            }
        },
        event: {
            keypress: function(key){
                //Can dispatch this 'keypress' event for a proper handler to execute in phantomJS

                var evt = document.createEvent('Event');
                evt.initEvent('keypress', true, false);
                evt.keyCode = key;
                return evt;
            },
            keydown: function(key){
                //Can dispatch this 'keydown' event for a proper handler to execute in phantomJS

                var evt = document.createEvent('Event');
                evt.initEvent('keydown', true, false);
                evt.keyCode = key;
                return evt;
            }
        }
    };
}(jQuery);

module.exports = baseTestHelper;