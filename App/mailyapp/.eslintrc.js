module.exports =
{
    'env':
    {
        'browser': true,
        'es6': true
    },
    'extends':
    [
        'plugin:vue/recommended',
        'eslint:recommended'
    ],
    'globals':
    {
        'Atomics': 'readonly',
        'SharedArrayBuffer': 'readonly'
    },
    'parserOptions':
    {
        'ecmaVersion': 2018,
        'sourceType': 'module'
    },
    'plugins':
    [
        'vue'
    ],
    'rules':
    {
		'vue/valid-v-model': ['off'],
		'vue/no-v-html': ['off'],
        'vue/html-indent': ['off', 'tab'],
        'vue/html-closing-bracket-spacing': ['off'],
        'vue/max-attributes-per-line': ['off'],
        'vue/html-quotes': ['warn', 'single'],
        'vue/no-unused-vars': ['warn'],
        'vue/return-in-computed-property': ['warn'],
        'vue/require-v-for-key': ['off'],
        'no-undef': ['off'],
        'no-unused-labels': ['warn'],
        'no-unused-vars': ['warn'],
		'no-mixed-spaces-and-tabs': ['off'],
		'no-unreachable': ['off'],
		'vue/comment-directive': ['off'] // broken
    }
}
