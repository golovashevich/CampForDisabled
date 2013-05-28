/// jQuery->Script Control adapter

var jQuery = {
	validator: {
		addMethod: function (methodName, method) {
		},

		unobtrusive: {
			adapters: {
				add: function (adapterName, params, fn) {
				}
			}
		}
	}
};


/// QUnit->Script Control adapter
var js_methods = {};
function test(name, method) {
	js_methods[name] = method;
}

function ok(value, message) {
	if (!value) {
		throw new Error(message);
	}
}

function execute(name) {
	js_methods[name]();
}
