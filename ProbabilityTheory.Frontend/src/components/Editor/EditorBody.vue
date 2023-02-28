<template>
<div>
    
    <div v-if="authorized" v-show="editing" class="row">
        <EditorMenu :editor="editor" />
    </div>

    <div class="row">
        <div :class="{ 'card-panel': !editing }">
            <EditorContent :editor="editor" />
        </div>
    </div>
    <div v-if="authorized" v-show="editing" class="character-count">
        {{ editor?.storage.characterCount.characters() }}/{{ limit }} {{ $t('editor.characters') }}
        <br>
        {{ editor?.storage.characterCount.words() }} {{ $t('editor.words') }}
    </div>

    <div v-if="authorized" v-show="editing" class="row right">
        <button @click="submit" class="btn waves-effect waves-light" name="action" style="margin-right: 8px">
            <span><SendIcon/> {{ $t('button.confirm') }}</span>
        </button>
        <button @click="cancel" class="btn waves-effect waves-light grey darken-1" name="action">
            <span><CancelIcon/> {{ $t('button.cancel') }}</span>
        </button>
    </div>
</div>
</template>


<script>
import EditorMenu from '@/components/Editor/EditorMenu.vue'

import { Editor, EditorContent } from '@tiptap/vue-3'
import StarterKit from '@tiptap/starter-kit'
import CharacterCount from '@tiptap/extension-character-count'
import Link from '@tiptap/extension-link'
import TextAlign from '@tiptap/extension-text-align'

import SendIcon from 'icons/Send'
import CancelIcon from 'icons/Cancel'

export default {
    name: "EditorBody",
    emits: ['cancelled', 'submited'],
    components: {
        Editor, EditorContent,
        EditorMenu,
        StarterKit,
        CharacterCount,
        Link,
        TextAlign,
        SendIcon, CancelIcon
    },
    data() {
        return {
            editor: null,
            editing: false,
            limit: 2500
        }
    },
    computed: {
        authorized() { return true}
    },
    methods: {
        submit() {
            let content = this.editor.getHTML()
            this.$emit('submited', content)
        },
        cancel() {
            this.toggleEditing()
            this.$emit('cancelled')
        },
        setContent(content) {
            this.editor.commands.setContent(content)
        },
        toggleEditing() {
            this.editing = !this.editing
            this.editor.setOptions({editable: this.editing})
        }
    },
    mounted() {
        this.editor = new Editor({
            editable: false,
            content: '-',
            extensions: [
                StarterKit,
                Link.configure({
                    openOnClick: false,
                }),
                TextAlign.configure({
                    types: ['heading', 'paragraph'],
                }),
                CharacterCount.configure({
                    limit: this.limit,
                })
            ]
        })
    },
    beforeUnmount() {
        this.editor.destroy()
    }
}
</script>


<style>
.btn-editor {
    margin: 2px
}
</style>