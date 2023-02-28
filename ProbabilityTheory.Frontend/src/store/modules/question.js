import axios from "axios"

export default {
    state: {
        question: {
            id: '0',
            chapterId: '0',
            description: '-',
            answer: '-'
        },
        questionIdToDelete: ''
    },
    getters: {
        question: (state) => {
            return state.question
        },
        questionIdToDelete: (state) => {
            return state.questionIdToDelete
        }
    },
    mutations: {
        setQuestion: (state, question) => {
            state.question = question
        },
        setQuestionDescription: (state, description) => {
            state.question.description = description
        },
        setQuestionAnswer: (state, answer) => {
            state.question.answer = answer
        },
        setQuestionIdToDelete: (state, id) => {
            state.questionIdToDelete = id
        }
    },
    actions: {
        getQuestion: ({commit}, params) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'get',
                    url: `questions/${params.questionId}`,
                    params
                }).then(resp => {
                    let question = resp.data
                    commit('setQuestion', question)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        createQuestion: ({commit}, payload) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'post',
                    url: 'questions',
                    data: payload
                }).then(resp => {
                    let question = {}
                    question['id'] = resp.data

                    commit('addQuestion', question)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        updateQuestion: ({commit}, payload) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'put',
                    url: 'questionTranslations',
                    data: payload
                }).then(resp => {
                    commit('setQuestionDescription', payload.description)
                    commit('setQuestionAnswer', payload.answer)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        deleteQuestion: ({commit}, params) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'delete',
                    url: `questions/${params.id}`,
                    params
                }).then(resp => {
                    commit('removeQuestionById', params.id)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        }
    }
}
