import axios from "axios"

export default {
    state: {
        terms: [],
        term: {
            id: '',
            title: '-',
            definition: '-'
        }
    },
    getters: {
        terms: (state) => {
            return state.terms
        },
        term: (state) => {
            return state.term
        },
        filterTermsByTitle: (state) => (title) => {
            title = title.toLowerCase()
            return state.terms.filter(t => t.title.toLowerCase().includes(title))
        }
    },
    mutations: {
        setTerms: (state, terms) => {
            state.terms = terms
        },
        setTerm: (state, term) => {
            state.term = term
        },
        setTermTranslation: (state, translation) => {
            state.term.title = translation.title
            state.term.definition = translation.definition

            let terms = state.terms
            for(let term of terms) {
                if(term.id === translation.termId) {
                    term.title = translation.title
                    return
                }
            }
        },
        addTerm: (state, term) => {
            state.terms.push(term)
        },
        removeTermById: (state, id) => {
            let i = 0
            for(let term of state.terms) {
                if(term.id === id) {
                    state.terms.splice(i, 1)
                    return
                }
                i++
            }
        }
    },
    actions: {
        createTerm: ({commit}, payload) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'post',
                    url: 'terms',
                    data: payload
                }).then(resp => {
                    let term = {}
                    term['id'] = resp.data
                    term['title'] = '-'
                    commit('addTerm', term)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        getTerms: ({commit}, params) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'get',
                    url: 'terms',
                    params
                }).then(resp => {
                    let terms = resp.data.terms
                    commit('setTerms', terms)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        getTerm: ({commit}, params) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'get',
                    url: `terms/${params.id}`,
                    params
                }).then(resp => {
                    let term = resp.data
                    commit('setTerm', term)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        updateTerm: ({commit}, payload) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'put',
                    url: 'termTranslations',
                    data: payload
                }).then(resp => {
                    commit('setTermTranslation', payload)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        deleteTerm: ({commit}, params) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'delete',
                    url: `terms/${params.id}`,
                    params
                }).then(resp => {
                    commit('removeTermById', params.id)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        }
    }
}
