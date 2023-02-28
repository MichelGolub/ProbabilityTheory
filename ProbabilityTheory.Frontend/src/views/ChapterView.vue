<template>
    <div class="container">
        <div class="row">
            <ChapterTitle :chapter="chapter"/>
        </div>
        <div class="row">
            <router-link :to="{ name: 'pages' }" :class="{ 'green-text': ['page', 'pages'].includes($route.name) }">
                {{ $t('theory') }}
            </router-link> |
            <router-link :to="{ name: 'questions' }" :class="{ 'green-text': ['question', 'questions'].includes($route.name) }">
                {{ $t('tests') }}
            </router-link> | 
            <router-link :to="{ name: 'exercises' }" :class="{ 'green-text': ['exercise', 'exercises'].includes($route.name) }">
                {{ $t('tasks') }}
            </router-link>
        </div>
        <div class="row">
            <router-view></router-view>
        </div>
    </div>
</template>


<script>
import { mapGetters, mapActions } from 'vuex'
import ChapterTitle from '@/components/Chapter/ChapterTitle.vue'

export default {
    name: 'ChapterView',
    components: {
        ChapterTitle
    },
    computed: {
        ...mapGetters([
            'chapter'
        ]),
        languageId() { return this.$store.getters.languageIdByCode(this.$i18n.locale) },
        id() { return this.$route.params.id }
    },
    methods: {
        ...mapActions([
            'getChapter'
        ]),
        loadChapter() {
            let languageId = this.languageId
            let id = this.id
            if(!languageId) {
                return
            }
            this.getChapter({id, languageId})
        },
    },
    mounted() {
        this.loadChapter()
    },
    watch: {
        languageId() {
            this.loadChapter()
        },
        id(newId) {
            if(!newId) {
                return
            }
            this.loadChapter()
        }
    }
}
</script>


<i18n>
{
    "eng": {
        "theory": "Theory",
        "tests": "Tests",
        "tasks": "Tasks"
    },
    "ukr": {
        "theory": "Теорія",
        "tests": "Тести",
        "tasks": "Завдання"
    }
}
</i18n>