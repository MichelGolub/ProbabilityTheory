<template>
<div class="container">
    <div v-show="!editing">
        <h4> 
            {{ term.title }} 
            <EditIcon @click="editing=true" v-if="authorized" style=" cursor: pointer"/>
        </h4>
        <p>{{ term.definition }} </p>
    </div>
    <div v-show="editing">
        <TermForm :term="term" v-on:done="editing=false"/>
    </div>
</div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'

import TermForm from '@/components/Term/TermForm.vue'

import EditIcon from 'icons/Pencil'

export default {
    name: 'TermView',
    components: {
        TermForm,
        EditIcon
    },
    data() {
        return {
            editing: false
        }
    },
    computed: {
        ...mapGetters([
            'term'
        ]),
        authorized() { return true },
        languageId() { return this.$store.getters.languageIdByCode(this.$i18n.locale) },
        id() { return this.$route.params.id }
    },
    methods: {
        ...mapActions([
            'getTerm',
            'updateTerm'
        ]),
        loadTerm() {
            let languageId = this.languageId
            let id = this.id
            if(!languageId) {
                return
            }
            this.getTerm({id, languageId})
        },
    },
    mounted() {
        this.loadTerm();
    },
    watch: {
        languageId() {
            this.loadTerm()
        }
    }
}
</script>