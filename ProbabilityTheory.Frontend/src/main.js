import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'

import axios from 'axios'
axios.defaults.baseURL = process.env.VUE_APP_API_BASE_URL

import i18n from '@/i18n'

import 'materialize-css/dist/js/materialize.min'

createApp(App)
    .use(store)
    .use(router)
    .use(i18n)
    .mount('#app')
