<template>
    <div id="chapterDeleteModal" class="modal">
        <div class="modal-content">
            <h4>{{ $t('message.warning') }}</h4>
            <p>{{ $t('message.delete') }}</p>
        </div>
        <div class="modal-footer">
            <a href="#!" class="modal-close waves-effect waves-green btn-flat">
                {{ $t('button.cancel') }}
            </a>
            <button @click="removeChapter" class="modal-close waves-effect waves-green btn-flat">
                {{ $t('button.confirm') }}
            </button>
        </div>
    </div>
</template>


<script>
import { mapActions } from 'vuex'

export default {
    name: "ChapterDeleteModal",
    methods: {
        ...mapActions([
            'deleteChapter'
        ]),
        open(id) {
            this.$store.commit('setChapter', {id})
            $('#chapterDeleteModal').modal('open')
        },
        removeChapter() {
            let id = this.$store.getters['chapter'].id
            this.deleteChapter({id})
            .then(() => {
                $('#chapterDeleteModal').modal('close')
            })
        }
    },
    mounted() {
        $('.modal').modal()
    }
}
</script>
