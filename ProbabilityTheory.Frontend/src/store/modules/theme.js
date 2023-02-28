import axios from "axios"

export default {
    state: {
        themes: [],
        theme: { id: '' }
    },
    getters: {
        themes: (state) => {
            return state.themes
        },
        theme: (state) => {
            return state.theme
        },
    },
    mutations: {
        setTheme: (state, theme) => {
            state.theme = theme
        },
        setThemes: (state, themes) => {
            state.themes = themes
        },
        addChapter: (state, chapter) => {
            let theme = state.themes.find(t => t.id === chapter.themeId)
            let chapterLookupDto = {
                id: chapter.id,
                title: chapter.title
            }
            theme.chapters.push(chapterLookupDto)
        },
        addTheme: (state, theme) => {
            state.themes.push(theme)
        },
        removeChapterById: (state, id) => {
            for(let theme of state.themes) {
                let idx = 0
                for(let chapter of theme.chapters) {
                    if(chapter.id === id) {
                        theme.chapters.splice(idx, 1)                        
                        return
                    }
                    idx++
                }
            }
        },
        setThemeTitleById: (state, payload) => {
            let theme = state.themes.find(t => t.id === payload.id)
            theme.title = payload.title
        },
        removeThemeById: (state, id) => {
            let idx = 0
            for(let theme of state.themes) {
                if(theme.id === id) {
                    break
                }
                idx++
            }
            state.themes.splice(idx, 1)
        },
        selectThemeById: (state, id) => {
            let theme = state.themes.find(t => t.id === id)
            state.theme = theme
        }
    },
    actions: {
        getThemes: ({ commit }, params) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'get',
                    url: 'themes',
                    params
                }).then(resp => {
                    commit('setThemes', resp.data.themes)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        createTheme: ({commit}, payload) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'post',
                    url: 'themes',
                    data: payload
                }).then(resp => {
                    let theme = {}
                    theme['id'] = resp.data
                    theme['title'] = '-'
                    theme['chapters'] = []
                    commit('addTheme', theme)

                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        updateTheme: ({commit}, payload) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'put',
                    url: 'themeTranslations',
                    data: payload
                }).then(resp => {
                    commit('setThemeTitleById', { 
                        id: payload.themeId,
                        title: payload.title 
                    })
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        deleteTheme: ({commit}, params) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'delete',
                    url: `themes/${params.id}`,
                }).then(resp => {
                    commit('removeThemeById', params.id)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        }
    }
}
