import { createStore } from 'vuex'

//import oidc from './modules/oidc'
import language from './modules/language'
import theme from './modules/theme'
import chapter from './modules/chapter'
import page from './modules/page'
import question from './modules/question'
import exercise from './modules/exercise'
import term from './modules/term'

export default createStore({
    modules: {
        //oidc,
        language,
        theme, chapter,
        page, question, exercise,
        term
    }
})