<template>
    <div class="card-panel">
        <div class="row">
            <ThemeTitle :theme="theme"/>
        </div>                    
        <div class="row" v-for="(chapter) in theme.chapters" :key="chapter.id">
            <router-link :to="{ name: 'pages', params: { id: chapter.id }}">
                {{ chapter.title }}
            </router-link>
            <button @click="askForRemoveChapter(chapter.id)" v-if="authorized" class="waves-effect waves-light btn-small red darken-1 right">
                <DeleteIcon/> {{ $t('chapter.remove') }}
            </button>
        </div>
        <div v-if="authorized" class="row">
            <button @click="addChapter(theme.id)" class="btn">
                <AddIcon/> {{ $t('chapter.add')}}
            </button>
        </div>
    </div>

<ChapterDeleteModal ref="chapterDeleteModal"/>
</template>

<script>
import { mapActions } from 'vuex'

import ThemeTitle from '@/components/Theme/ThemeTitle.vue'

import ChapterDeleteModal from '@/components/Chapter/ChapterDeleteModal.vue'

import DeleteIcon from 'icons/AlertCircle'
import AddIcon from 'icons/PlusCircle'

export default {
    name: 'ThemeCardPanel',
    props: ['theme'],
    components: {
        ThemeTitle,
        ChapterDeleteModal,
        DeleteIcon, AddIcon
    },
    computed: {
        authorized() { return true }
    },
    methods: {
        ...mapActions([
            'createChapter',
            'deleteChapter'
        ]),
        addChapter(themeId) {
            this.createChapter({themeId})
        },
        askForRemoveChapter(id) {
            this.$refs.chapterDeleteModal.open(id)
        }
    },
    mounted() {
        $('.modal').modal();
    }
}
</script>


<i18n>
{
    "eng": {
        "chapter": {
            "remove": "delete",
            "add": "add chapter"
        }
    },
    "ukr": {
        "chapter": {
            "remove": "видалити",
            "add": "додати розділ"
        }
    }
}
</i18n>