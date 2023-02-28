import axios from "axios"

export default {
    state: {
        chapter: {
            id: 0,
            title: '',
            themeId: 0,
            pages: [],
            exercises: [],
            questions: []
        }
    },
    getters: {
        chapter: (state) => {
            return state.chapter
        }
    },
    mutations: {
        setChapter: (state, chapter) => {
            state.chapter = chapter
        },
        setChapterTitle: (state, title) => {
            state.chapter.title = title
        },
        addPage: (state, page) => {
            let pageLookupDto = {
                id: page.id
            }
            state.chapter.pages.push(pageLookupDto)
        },
        addQuestion: (state, question) => {
            let questionLookupDto = {
                id: question.id
            }
            state.chapter.questions.push(questionLookupDto)
        },
        addExercise: (state, exercise) => {
            let exerciseLookupDto = {
                id: exercise.id
            }
            state.chapter.exercises.push(exerciseLookupDto)
        },
        removePageById: (state, id) => {
            let idx = 0
            for(let page of state.chapter.pages) {
                if(page.id === id) {
                    state.chapter.pages.splice(idx, 1)
                    return
                }
                idx++
            }
        },
        removeQuestionById: (state, id) => {
            let idx = 0
            for(let question of state.chapter.questions) {
                if(question.id === id) {
                    state.chapter.questions.splice(idx, 1)
                    return
                }
                idx++
            }
        },
        removeExerciseById: (state, id) => {
            let idx = 0
            for(let exercise of state.chapter.exercises) {
                if(exercise.id === id) {
                    state.chapter.exercises.splice(idx, 1)
                    return
                }
                idx++
            }
        },
    },
    actions: {
        getChapter: ({commit}, params) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'get',
                    url: `chapters/${params.id}`,
                    params: {
                        languageId: params.languageId
                    }
                }).then(resp => {
                    commit('setChapter', resp.data)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        createChapter: ({commit}, payload) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'post',
                    url: 'chapters',
                    data: payload
                }).then(resp => {
                    let chapter = {}
                    chapter['id'] = resp.data
                    chapter['title'] = '-'
                    chapter['themeId'] = payload.themeId
                    commit('addChapter', chapter)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        updateChapter: ({commit}, payload) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'put',
                    url: 'chapterTranslations',
                    data: payload
                }).then(resp => {
                    commit('setChapterTitle', payload.title)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        deleteChapter: ({commit}, params) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'delete',
                    url: `chapters/${params.id}`,
                    params
                }).then(resp => {
                    commit('removeChapterById', params.id)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        }
    }
}
