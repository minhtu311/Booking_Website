module('Selection containers - Multiple');

var MultipleSelection = require('select2/selection/multiple');

var $ = require('jquery');
var Options = require('select2/options');
var Utils = require('select2/utils');

var options = new Options({});

test('display uses templateSelection', function (assert) {
    var called = false;

    var templateOptions = new Options({
        templateSelection: function (data) {
            called = true;

            return data.text;
        }
    });

    var selection = new MultipleSelection(
        $('#qunit-fixture .multiple'),
        templateOptions
    );

    var out = selection.display({
        text: 'test'
    });

    assert.ok(called);

    assert.equal(out, 'test');
});

test('templateSelection can addClass', function (assert) {
    var called = false;

    var templateOptions = new Options({
        templateSelection: function (data, container) {
            called = true;
            container.addClass('testclass');
            return data.text;
        }
    });

    var selection = new MultipleSelection(
        $('#qunit-fixture .multiple'),
        templateOptions
    );

    var $container = selection.selectionContainer();

    var out = selection.display({
        text: 'test'
    }, $container);

    assert.ok(called);

    assert.equal(out, 'test');

    assert.ok($container.hasClass('testclass'));
});

test('empty update clears the selection', function (assert) {
    var selection = new MultipleSelection(
        $('#qunit-fixture .multiple'),
        options
    );

    var $selection = selection.render();
    var $rendered = $selection.find('.select2-selection__rendered');

    $rendered.text('testing');

    selection.update([]);

    assert.equal(
        $rendered.text(),
        '',
        'There should have been nothing rendered'
    );
});

test('empty update clears the selection title', function (assert) {
    var selection = new MultipleSelection(
        $('#qunit-fixture .multiple'),
        options
    );

    var $selection = selection.render();

    selection.update([]);

    var $rendered = $selection.find('.select2-selection__rendered li');

    assert.equal(
        $rendered.attr('title'),
        undefined,
        'The title should be removed if nothing is rendered'
    );
});

test('update sets the title to the data text', function (assert) {
    var selection = new MultipleSelection(
        $('#qunit-fixture .multiple'),
        options
    );

    var $selection = selection.render();

    selection.update([{
        text: 'test'
    }]);

    var $rendered = $selection.find('.select2-selection__rendered li');

    assert.equal(
        $rendered.attr('title'),
        'test',
        'The title should have been set to the text'
    );
});

test('update sets the title to the data title', function (assert) {
    var selection = new MultipleSelection(
        $('#qunit-fixture .multiple'),
        options
    );

    var $selection = selection.render();

    selection.update([{
        text: 'test',
        title: 'correct'
    }]);

    var $rendered = $selection.find('.select2-selection__rendered li');

    assert.equal(
        $rendered.attr('title'),
        'correct',
        'The title should have taken precedence over the text'
    );
});

test('update should clear title for placeholder options', function (assert) {
    var selection = new MultipleSelection(
        $('#qunit-fixture .multiple'),
        options
    );

    var $selection = selection.render();

    selection.update([{
        id: '',
        text: ''
    }]);

    var $rendered = $selection.find('.select2-selection__rendered li');

    assert.equal(
        $rendered.attr('title'),
        undefined,
        'The title should be removed if a placeholder is rendered'
    );
});

test('update should clear title for options without text', function (assert) {
    var selection = new MultipleSelection(
        $('#qunit-fixture .multiple'),
        options
    );

    var $selection = selection.render();

    selection.update([{
        id: ''
    }]);

    var $rendered = $selection.find('.select2-selection__rendered li');

    assert.equal(
        $rendered.attr('title'),
        undefined,
        'The title should be removed if there is no text or title property'
    );
});

test('escapeMarkup is being used', function (assert) {
    var selection = new MultipleSelection(
        $('#qunit-fixture .multiple'),
        options
    );

    var $selection = selection.render();
    var $rendered = $selection.find('.select2-selection__rendered');

    var unescapedText = '<script>bad("stuff");</script>';

    selection.update([{
        text: unescapedText
    }]);

    assert.equal(
        $rendered.text().substr(1),
        unescapedText,
        'The text should be escaped by default to prevent injection'
    );
});

test('clear button respects the disabled state', function (assert) {
    var options = new Options({
        disabled: true
    });

    var $select = $('#qunit-fixture .multiple');

    var container = new MockContainer();
    var $container = $('<div></div>');

    var selection = new MultipleSelection(
        $select,
        options
    );

    var $selection = selection.render();
    $container.append($selection);

    selection.bind(container, $container);

    // Select an option
    selection.update([{
        text: 'Test'
    }]);

    var $rendered = $selection.find('.select2-selection__rendered');

    var $pill = $rendered.find('.select2-selection__choice');

    assert.equal($pill.length, 1, 'There should only be one selection');

    var $remove = $pill.find('.select2-selection__choice__remove');

    assert.equal(
        $remove.length,
        1,
        'The remove icon is displayed for the selection'
    );

    // Set up the unselect handler
    selection.on('unselect', function (params) {
        assert.ok(false, 'The unselect handler should not be triggered');
    });

    // Trigger the handler for the remove icon
    $remove.trigger('click');
});