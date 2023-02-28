<template>
    <div class="container">
        <div class="row">
            <h3>{{ $t('theory') }}</h3>
            <a @click="addTheme" v-if="authorized" style="cursor: pointer">
                {{ $t('theme.add') }}
            </a>
        </div>
        <div v-for="(theme) in themes" :key="theme.id" class="row">
            <ThemeCardPanel :theme="theme"/>
        </div>
    </div>
</template>


<script>
import { mapGetters, mapActions } from 'vuex'
import ThemeCardPanel from '@/components/Theme/ThemeCardPanel.vue'

export default {
    name: "ChaptersView",
    components: {
        ThemeCardPanel
    },
    computed: {
        ...mapGetters([
            'themes'
        ]),
        authorized() { return true },
        languageId() { return this.$store.getters.languageIdByCode(this.$i18n.locale) }
    },
    methods: {
        ...mapActions([
            'getThemes',
            'createTheme'
        ]),
        addTheme() {
            this.createTheme()
        },
        loadThemes() {
            let languageId = this.languageId
            if(!languageId) {
                return
            }
            this.getThemes({languageId})
        },
    },
    mounted() {
        this.loadThemes()
    },
    watch: {
        languageId() {
            this.loadThemes()
        }
    }
}
</script>


<i18n>
{
    "eng": {
        "theory": "Theory",
        "theme": {
            "add": "Create new theme"
        }
    },
    "ukr": {
        "theory": "Теорія",
        "theme": {
            "add": "Створити нову тему"
        }
    }
}
</i18n>