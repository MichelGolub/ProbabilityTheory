<template>
    <div id="questionDeleteModal" class="modal">
        <div class="modal-content">
            <h4>{{ $t('message.warning') }}</h4>
            <p>{{ $t('message.delete') }}</p>
        </div>
        <div class="modal-footer">
            <a href="#!" class="modal-close waves-effect waves-green btn-flat">
                {{ $t('button.cancel') }}
            </a>
            <button @click="removeQuestion" class="modal-close waves-effect waves-green btn-flat">
                {{ $t('button.confirm') }}
            </button>
        </div>
    </div>
</template>


<script>
import { mapActions } from 'vuex'

export default {
    name: "QuestionDeleteModal",
    methods: {
        ...mapActions([
            'deleteQuestion'
        ]),
        open(id) {
            this.$store.commit('setQuestionIdToDelete', id)
            $('#questionDeleteModal').modal('open')
        },
        removeQuestion() {
            let id = this.$store.getters['questionIdToDelete']
            this.deleteQuestion({id})
            .then(() => {
                $('#questionDeleteModal').modal('close')
            })
        }
    },
    mounted() {
        $('.modal').modal()
    }
}
</script>