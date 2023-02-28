<template>
<div v-if="authorized" v-show="!editing" class="row">
    <button @click="editing=true"  class="waves-effect waves-light btn-small" style="margin-right: 8px">
        <EditIcon/> {{ $t('page.edit' )}}
    </button>
    <button @click="askForRemovePage" class="waves-effect waves-light btn-small red darken-1">
        <DeleteIcon/> {{ $t('page.delete' )}}
    </button>
</div>

<div class="row">
    <Editor ref="editor" v-on:submited="update" v-on:cancelled="setContent"/>
</div>

<PageDeleteModal ref="pageDeleteModal"/>
</template>


<script>
import { mapGetters, mapActions } from 'vuex'

import Editor from '@/components/Editor/EditorBody.vue'
import PageDeleteModal from '@/components/Page/PageDeleteModal.vue'

import DeleteIcon from 'icons/AlertCircle'
import EditIcon from 'icons/Pencil'

export default {
    name: 'PageView',
    components: {
        Editor,
        PageDeleteModal,
        DeleteIcon, EditIcon
    },
    data() {
        return {
            editing: false,
        }
    },
    computed: {
        ...mapGetters([
            'page'
        ]),
        authorized() { return true },
        languageId() { return this.$store.getters.languageIdByCode(this.$i18n.locale) },
        pageId() { return this.$route.params.pageId }
    },
    methods: {
        ...mapActions([
            'getPage',
            'deletePage',
            'updatePage'
        ]),
        askForRemovePage() {
            this.$refs.pageDeleteModal.open(this.pageId)
        },
        load() {
            let languageId = this.languageId
            let pageId = this.pageId
            return new Promise((resolve, reject) => {
                this.getPage({languageId, pageId})
                .then(() => {
                    this.setContent()
                })
            })
        },
        setContent() {
            let page = this.page
            this.$refs.editor.setContent(page.content)
        },
        update(content) {
            let payload = {}
            payload['languageId'] = this.languageId
            payload['pageId'] = this.pageId
            payload['content'] = content

            this.updatePage(payload)
            .then(() => {
                this.editing = false
            })
        }
    },
    mounted() {
        this.load()
    },
    watch: {
        languageId() {
            this.load()
        },
        pageId(newPageId) {
            if(!newPageId) {
                return
            }
            this.load()
        },
        editing() {
            this.$refs.editor.toggleEditing()
        }
    }
}
</script>


<i18n>
{
    "eng": {
        "page": {
            "edit": "edit",
            "delete": "delete"
        }
    },
    "ukr": {
        "page": {
            "edit": "редагувати",
            "delete": "видалити"
        }
    }
}
</i18n>