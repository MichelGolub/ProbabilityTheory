import axios from "axios"

export default {
    state: {
        exercise: {
            id: '',
            chapterId: '',
            description: '-',
            steps: []
        },
        exerciseIdToDelete: ''
    },
    getters: {
        exercise: (state) => {
            return state.exercise
        },
        exerciseIdToDelete: (state) => {
            return state.exerciseIdToDelete
        }
    },
    mutations: {
        setExercise: (state, exercise) => {
            state.exercise = exercise
        },
        setExerciseDescription: (state, description) => {
            state.exercise.description = description
        },
        addStep: (state, step) => {
            let stepLookupDto = {
                id: step.id,
                description: step.description
            }
            state.exercise.steps.push(stepLookupDto)
        },
        setExerciseIdToDelete: (state, id) => {
            state.exerciseIdToDelete = id
        },
        addStep: (state, step) => {
            state.exercise.steps.push(step)
        },
        setStepDescriptionById: (state, payload) => {
            for(let step of state.exercise.steps) {
                if(step.id === payload.id) {
                    step.description = payload.description
                }
            }
        },
        removeStepById: (state, id) => {
            let idx = 0
            for(let step of state.exercise.steps) {
                if(step.id === id) {
                    state.exercise.steps.splice(idx, 1)
                    return
                }
                idx++
            }
        }
    },
    actions: {
        getExercise: ({commit}, params) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'get',
                    url: `exercises/${params.exerciseId}`,
                    params
                }).then(resp => {
                    let exercise = resp.data
                    commit('setExercise', exercise)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        createExercise: ({commit}, payload) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'post',
                    url: 'exercises',
                    data: payload
                }).then(resp => {
                    let exercise = {}
                    exercise['id'] = resp.data

                    commit('addExercise', exercise)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        updateExercise: ({commit}, payload) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'put',
                    url: 'exerciseTranslations',
                    data: payload
                }).then(resp => {
                    commit('setExerciseDescription', payload.description)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        deleteExercise: ({commit}, params) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'delete',
                    url: `exercises/${params.id}`,
                    params
                }).then(resp => {
                    commit('removeExerciseById', params.id)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        createStep: ({commit}, payload) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'post',
                    url: 'steps',
                    data: payload
                }).then(resp => {
                    let step = {}
                    step['id'] = resp.data
                    step['description'] = '-'
                    commit('addStep', step)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        updateStep: ({commit}, payload) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'put',
                    url: 'stepTranslations',
                    data: payload
                }).then(resp => {
                    commit('setStepDescriptionById', { 
                        id: payload.stepId,
                        description: payload.description 
                    })
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        deleteStep: ({commit}, params) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'delete',
                    url: `steps/${params.id}`,
                }).then(resp => {
                    commit('removeStepById', params.id)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        }
    }
}