const { defineConfig } = require('@vue/cli-service')
const webpack = require('webpack')
const path = require('path');

module.exports = defineConfig({
  transpileDependencies: true,
  configureWebpack: {
    plugins: [
      new webpack.ProvidePlugin({
        $: 'jquery',
        jquery: 'jquery',
        'window.jQuery': 'jquery',
        jQuery: 'jquery'
      })
    ],
    resolve: {
      alias: {
        'icons': path.resolve(__dirname, 'node_modules/vue-material-design-icons')
      },
      extensions: [
        ".vue"
      ]
    }
  },
  pluginOptions: {
    i18n: {
      locale: 'eng',
      fallbackLocale: 'eng',
      localeDir: 'locales',
      enableInSFC: false
    }
  },
  chainWebpack: config => {
    config.module
      .rule("i18n")
      .resourceQuery(/blockType=i18n/)
      .type('javascript/auto')
      .use("i18n")
        .loader("@intlify/vue-i18n-loader")
        .end();
  }
})
