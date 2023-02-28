<template>
    <div>
        <div class="row">
            <a @click="addQuestion" v-if="authorized" style="cursor: pointer">
                {{ $t('question.add') }}
            </a>
        </div>

        <div class="row">
            <router-view></router-view>
        </div>

        <div class="divider"></div>
        <div class="row">
            <div class="center-align">
                <ul class="pagination">
                    <li v-for="(question, index) in questions" :key="index"
                    :class="{ active: $route.params['questionId'] === question.id }"
                    class="waves-effect">
                        <router-link :to="{ name: 'question', params: { questionId: question.id } }">
                            {{ index + 1 }}
                        </router-link>
                    </li>
                </ul>
            </div>
        </div>

        <div class="row">
            <h5 v-show="!questions?.length">{{ $t('question.noQuestions') }}</h5>
        </div>
    </div>
</template>


<script>
import { mapGetters, mapActions } from 'vuex'

export default {
    name: 'QuestionsView',
    computed: {
        ...mapGetters([
            'chapter'
        ]),
        questions() { return this.chapter.questions },
        questionsLength() { return this.questions.length },
        authorized() { return true },
        languageId() { return this.$store.getters.languageIdByCode(this.$i18n.locale) },
        id() { return this.$route.params.id }
    },
    methods: {
        ...mapActions([
            'createQuestion'
        ]),
        addQuestion() {
            let chapterId = this.id
            this.createQuestion({chapterId})
            .then((resp) => {
                this.$router.push({ name: 'question', params: { questionId: resp.data } })
            })
        },
        moveToFirstQuestionIfExist() {
            if (this.questions.length) {
                let question = this.questions[0]
                let route = this.$router.resolve({
                    name: 'question',
                    params: { questionId: question.id }
                })
                this.$router.push(route)
            }
        }
    },
    mounted() {
        this.moveToFirstQuestionIfExist()
    },
    watch: {
        questionsLength() {
            this.moveToFirstQuestionIfExist()
        }
    }
}
</script>


<i18n>
{
    "eng": {
        "question": {
            "add": "Add new question",
            "noQuestions": "There is no questions"
        }
    },
    "ukr": {
        "question": {
            "add": "Додати запитання",
            "noQuestions": "Немає запитань"
        }
    }
}
</i18n>