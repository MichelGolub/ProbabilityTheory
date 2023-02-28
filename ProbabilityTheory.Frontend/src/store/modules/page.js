import axios from "axios"

export default {
    state: {
        page: {
            id: '0',
            chapterId: '0',
            content: '-'
        },
        pageIdToDelete: ''
    },
    getters: {
        page: (state) => {
            return state.page
        },
        pageIdToDelete: (state) => {
            return state.pageIdToDelete
        }
    },
    mutations: {
        setPage: (state, page) => {
            state.page = page
        },
        setPageContent: (state, content) => {
            state.page.content = content
        },
        setPageIdToDelete: (state, id) => {
            state.pageIdToDelete = id
        }
    },
    actions: {
        getPage: ({commit}, params) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'get',
                    url: `pages/${params.pageId}`,
                    params
                }).then(resp => {
                    let page = resp.data
                    commit('setPage', page)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        createPage: ({commit}, payload) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'post',
                    url: 'pages',
                    data: payload
                }).then(resp => {
                    let page = {}
                    page['id'] = resp.data

                    commit('addPage', page)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        updatePage: ({commit}, payload) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'put',
                    url: 'pageTranslations',
                    data: payload
                }).then(resp => {
                    commit('setPageContent', payload.content)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        },
        deletePage: ({commit}, params) => {
            return new Promise((resolve, reject) => {
                axios({
                    method: 'delete',
                    url: `pages/${params.id}`,
                    params
                }).then(resp => {
                    commit('removePageById', params.id)
                    resolve(resp)
                }).catch(err => {
                    reject(err)
                })
            })
        }
    }
}
