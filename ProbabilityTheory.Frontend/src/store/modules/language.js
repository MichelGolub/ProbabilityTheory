import axios from "axios"

export default {
    state: {
        languages: [],
    },
    getters: {
        languages: (state) => {
            return state.languages
        },
        languageByCode: (state) => (code) => {
            return state.languages.find(l => l.code === code)
        },
        languageIdByCode: (state) => (code) => {
            let language = state.languages.find(l => l.code === code)
            return language?.id
        }
    },
    mutations: {
        setLanguages: (state, languages) => {
            state.languages = languages
        }
    },
    actions: {
        getLanguages: ({commit}) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'get',
                    url: 'languages'
                }).then(resp => {
                    commit('setLanguages', resp.data.languages)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        }
    }
}
