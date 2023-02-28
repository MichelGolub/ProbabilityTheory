<template>
<h4>
    {{ $t('description') }} 
    <EditIcon @click="editingD=true" v-if="authorized" style=" cursor: pointer"/> | 
    <button @click="askForRemoveQuestion" v-if="authorized" class="waves-effect waves-light btn-small red darken-1">
        <DeleteIcon/> {{ $t('question.delete' )}}
    </button>
</h4>
<div class="row">
    <Editor ref="editorD" v-on:submited="updateDescription" v-on:cancelled="setDescription"/>
</div>

<h4>
    {{ $t('answer') }} 
    <EditIcon @click="() => {editingA=true;show=true}" v-if="authorized" style=" cursor: pointer"/>
</h4>
<a @click="show=!show" v-show="!editingA" style="cursor: pointer">{{ $t('toggle') }}</a>
<div v-show="show" class="row">
    <Editor ref="editorA" v-on:submited="updateAnswer" v-on:cancelled="setAnswer"/>
</div>

<QuestionDeleteModal ref="questionDeleteModal"/>
</template>


<script>
import { mapGetters, mapActions } from 'vuex'

import Editor from '@/components/Editor/EditorBody.vue'
import QuestionDeleteModal from '@/components/Question/QuestionDeleteModal.vue'

import DeleteIcon from 'icons/AlertCircle'
import EditIcon from 'icons/Pencil'

export default {
    name: 'QuestionView',
    components: {
        Editor,
        QuestionDeleteModal,
        DeleteIcon, EditIcon
    },
    data() {
        return {
            editingA: false,
            editingD: false,
            show: false
        }
    },
    computed: {
        ...mapGetters([
            'question'
        ]),
        authorized() { return true },
        languageId() { return this.$store.getters.languageIdByCode(this.$i18n.locale) },
        questionId() { return this.$route.params.questionId }
    },
    methods: {
        ...mapActions([
            'getQuestion',
            'deleteQuestion',
            'updateQuestion'
        ]),
        askForRemoveQuestion() {
            this.$refs.questionDeleteModal.open(this.questionId)
        },
        load() {
            let languageId = this.languageId
            let questionId = this.questionId
            return new Promise(() => {
                this.getQuestion({languageId, questionId})
                .then(() => {
                    this.setDescription()
                    this.setAnswer()
                })
            })
        },
        setDescription() {
            let question = this.question
            this.$refs.editorD.setContent(question.description)
            this.editingD = false
            this.$refs.editorA.toggleEditing()
        },
        setAnswer() {
            let question = this.question
            this.$refs.editorA.setContent(question.answer)
            this.editingA = false
            this.$refs.editorA.toggleEditing()
        },
        updateDescription(content) {
            let payload = {}
            payload['languageId'] = this.languageId
            payload['questionId'] = this.questionId
            payload['description'] = content
            payload['answer'] = this.question.answer

            this.updateQuestion(payload)
            .then(() => {
                this.editingD = false
            })
        },
        updateAnswer(content) {
            let payload = {}
            payload['languageId'] = this.languageId
            payload['questionId'] = this.questionId
            payload['description'] = this.question.description
            payload['answer'] = content

            this.updateQuestion(payload)
            .then(() => {
                this.editingA = false
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
        questionId(newId) {
            if(!newId) {
                return
            }
            this.load()
        },
        editingA() {
            this.$refs.editorA.toggleEditing()
        },
        editingD() {
            this.$refs.editorD.toggleEditing()
        }
    }
}
</script>


<i18n>
{
    "eng": {
        "question": {
            "edit": "edit",
            "delete": "delete this question"
        },
        "description": "Description",
        "answer": "Answer",
        "toggle": "Toggle"
    },
    "ukr": {
        "question": {
            "edit": "редагувати",
            "delete": "видалити це питання"
        },
        "description": "Питання",
        "answer": "Відповідь",
        "toggle": "Переключити"
    }
}
</i18n>