<template>
    <div class="container">
        <div class="row">
            <h3>{{ $t('terms') }}</h3>
            <a @click="addTerm" v-if="authorized" style="cursor: pointer">{{ $t('term.add') }}</a>
        </div>
        <div class="row">
            <div class="input-field">
                <SearchIcon class="material-icons prefix"/>
                <input id="search" type="search" v-model="filter.title" @input="filterTerms">
                <label for="search">{{ $t('search') }}</label>
            </div>
        </div>

        <ul class="collection">
            <li class="collection-item" v-for="term in terms" :key="term.id">
                <a style="cursor: pointer" @click="openTermModal(term.id)">
                    {{ term.title }}
                </a>
                <a @click="askToDeleteTerm(term.id)" v-if="authorized" class="red-text darken-1 secondary-content" style="cursor: pointer">
                    <DeleteIcon/> {{ $t('term.delete') }}
                </a>
            </li>
        </ul>

        <div class="row" v-show="!terms.length">
            <h4>{{ $t('term.noTerms') }}</h4>
        </div>
    </div>

<TermDetailsModal ref="term"/>
<TermDeleteModal ref="termDeleteModal" v-on:done="filterTerms"/>
</template>


<script>
import { mapActions, mapGetters } from 'vuex'

import TermDetailsModal from '@/components/Term/TermDetailsModal.vue'
import TermDeleteModal from '@/components/Term/TermDeleteModal.vue'

import SearchIcon from 'icons/Magnify'
import DeleteIcon from 'icons/AlertCircle'

export default {
    name: 'TermsView',
    components: {
        TermDetailsModal,
        TermDeleteModal,
        SearchIcon,
        DeleteIcon
    },
    data() {
        return {
            filter: {
                title: ''
            },
            terms: []
        }
    },
    computed: {
        ...mapGetters([
            'filterTermsByTitle'
        ]),
        authorized() { return true },
        languageId() {  return this.$store.getters.languageIdByCode(this.$i18n.locale) }
    },
    methods: {
        ...mapActions([
            'getTerms',
            'createTerm'
        ]),
        addTerm() {
            this.createTerm()
            .then(() => {
                this.filterTerms()
            })
        },
        askToDeleteTerm(id) {
            this.$refs.termDeleteModal.open(id)
        },
        loadTerms() {
            let languageId = this.languageId
            if(!languageId) {
                return
            }
            this.getTerms({languageId})
            .then(() => {
                this.filterTerms()
            })
        },
        filterTerms() {
            let title = this.filter.title
            this.terms = this.filterTermsByTitle(title)
        },
        openTermModal(id) {
            this.$refs.term.open(id)
        }
    },
    mounted() {
        this.loadTerms()
    },
    watch: {
        languageId() {
            this.loadTerms()
        }
    }
}
</script>


<i18n>
{
    "eng": {
        "terms": "Terms",
        "search": "Search",
        "term": {
            "add": "Add new term",
            "delete": "delete",
            "noTerms": "There are no terms"
        }
    },
    "ukr": {
        "terms": "Терміни",
        "search": "Пошук",
        "term": {
            "add": "Створити новий термін",
            "delete": "видалити",
            "noTerms": "Термінів немає"
        }
    }
}
</i18n>